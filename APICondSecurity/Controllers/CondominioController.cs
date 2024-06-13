using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult> CadastrarCondominio(CondominioDTO condominioDTO)
        {
            var condominio = _mapper.Map<Condominio>(condominioDTO);
            _condominioRepository.Incluir(condominio);
            try
            {
                await _condominioRepository.SaveAllAsync();
                return Ok("Condominio cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar o condominio: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        [Authorize]
        public async Task<ActionResult> UpdateCondominio(CondominioDTO condominioDTO)
        {
            if (condominioDTO.IdCondominio == null)
            {
                return BadRequest("Não é possivel alterar o condominio. É necessário informar o Id");
            }

            var condominioExiste = await _condominioRepository.Get(condominioDTO.IdCondominio);
            if (condominioExiste == null)
            {
                return NotFound("Condominio não identificado");
            }
            condominioExiste.Nome = condominioDTO.Nome;
            condominioExiste.Situacao = condominioDTO.Situacao;

            try
            {
                _condominioRepository.Alterar(condominioExiste);
                await _condominioRepository.SaveAllAsync();
                return Ok("Condominio alterado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar o condominio: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<ActionResult> Delete(int IdCondominio)
        {
            var condominio = _condominioRepository.Get(IdCondominio);
            if (condominio == null)
            {
                return NotFound("Id do condominio não encontrado.");
            }
            _condominioRepository.Excluir(await condominio);
            try
            {
                await _condominioRepository.SaveAllAsync();
                return Ok("Condominio excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir o condominio: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<CondominioRepository>>> GetCondominio()
        {
            return Ok(await _condominioRepository.GetAll());
        }
    }
}
