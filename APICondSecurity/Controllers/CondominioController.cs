using APICondSecurity.DTOs;
using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CondominioController : Controller
    {
        private readonly CondominioRepository _condominioRepository;
        private readonly IMapper _mapper;
        public CondominioController(CondominioRepository condominioRepository, IMapper mapper)
        {
            _mapper = mapper;
            _condominioRepository = condominioRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarCondominio(Condominio condominio)
        {
            _condominioRepository.Incluir(condominio);
            try
            {
                await _condominioRepository.SaveAllAsync();
                return Ok("Condominio cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a condominio: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateCondominio(Condominio condominio)
        {
            _condominioRepository.Alterar(condominio);
            try
            {
                await _condominioRepository.SaveAllAsync();
                return Ok("Condominio alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a condominio: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdCondominio)
        {
            var condominio = _condominioRepository.Get(IdCondominio);
            if (condominio == null)
            {
                return NotFound("Id da condominio não encontrado.");
            }
            _condominioRepository.Excluir(await condominio);
            try
            {
                await _condominioRepository.SaveAllAsync();
                return Ok("Condominio excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a condominio: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<CondominioRepository>> Get(int IdCondominio)
        {
            var condominio = await _condominioRepository.Get(IdCondominio);
            if (condominio == null)
            {
                return NotFound("Condominio Não encontrada para o Id informado.");
            }
            var condominioDTO = _mapper.Map<CondominioDTO>(condominio);
            return Ok(condominioDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CondominioRepository>>> GetCondominio()
        {
            return Ok(await _condominioRepository.GetAll());
        }
    }
}
