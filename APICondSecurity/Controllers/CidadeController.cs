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
    public class CidadeController : Controller
    {
        private readonly CidadeRepository _cidadeRepository;
        private readonly IMapper _mapper;
        public CidadeController(CidadeRepository cidadeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cidadeRepository = cidadeRepository;
        }


        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarCidade(Cidade cidade)
        {
            _cidadeRepository.Incluir(cidade);
            try
            {
                await _cidadeRepository.SaveAllAsync();
                return Ok("Cidade cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a cidade: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateCidade(Cidade cidade)
        {
            _cidadeRepository.Alterar(cidade);
            try
            {
                await _cidadeRepository.SaveAllAsync();
                return Ok("Cidade alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a cidade: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdCidade)
        {
            var cidade = _cidadeRepository.Get(IdCidade);
            if (cidade == null)

            {
                return NotFound("Id da cidade não encontrado.");
            }
            _cidadeRepository.Excluir(await cidade);
            try
            {
                await _cidadeRepository.SaveAllAsync();
                return Ok("Cidade excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a cidade: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<CidadeRepository>> Get(int IdCidade)
        {
            var cidade = await _cidadeRepository.Get(IdCidade);
            if (cidade == null)
            {
                return NotFound("Cidade Não encontrada para o Id informado.");
            }
            var cidadeDTO = _mapper.Map<CidadeDTO>(cidade);
            return Ok(cidadeDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CidadeRepository>>> GetCidade()
        {
            return Ok(await _cidadeRepository.GetAll());
        }
    }
}
