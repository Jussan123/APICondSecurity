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
    public class CidadeController(CidadeRepository cidadeRepository, IMapper mapper) : Controller
    {
        private readonly CidadeRepository _cidadeRepository = cidadeRepository;
        private readonly IMapper _mapper = mapper;

        [HttpPost("Cadastrar")]
        [Authorize]
        public async Task<ActionResult> CadastrarCidade(CidadeDTO cidadeDTO)
        {
            

            try
            { 
            var cidadeIBGEExiste = await _cidadeRepository.Get(cidadeDTO.CidadeIbge);
            if (cidadeIBGEExiste != null)
            {
                return NotFound("Codigo IBGE já cadastrado");
            }
            var cidade = _mapper.Map<Cidade>(cidadeDTO);
            _cidadeRepository.Incluir(cidade);
                await _cidadeRepository.SaveAllAsync();
                return Ok("Cidade cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a cidade: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        [Authorize]
        public async Task<ActionResult> UpdateCidade(CidadeDTO cidadeDTO)
        {
            if (cidadeDTO.IdCidade == null)
            {
                return BadRequest("Não é possível alterar a cidade. É necessário informar o ID.");
            }
            var cidadeExiste = await _cidadeRepository.GetId(cidadeDTO.IdCidade);

            if (cidadeExiste == null)
            {
                return NotFound("Cidade não identificada.");
            }

            var cidadeIBGEExiste = await _cidadeRepository.GetByIBGE(cidadeDTO.CidadeIbge);
            if (cidadeIBGEExiste != null && cidadeIBGEExiste.IdCidade != cidadeDTO.IdCidade)
            {
                return BadRequest("Codigo IBGE já cadastrado");
            }

            cidadeExiste.Nome = cidadeDTO.Nome;
            cidadeExiste.CidadeIbge = cidadeDTO.CidadeIbge;

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
        [Authorize]
        public async Task<ActionResult> Delete(int IdCidade)
        {
            var cidade = _cidadeRepository.GetId(IdCidade);
            if (cidade == null)

            {
                return NotFound("Id da cidade não encontrada.");
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
        [Authorize]
        public async Task<ActionResult<CidadeRepository>> Get(int IdCidade)
        {
            var cidade = await _cidadeRepository.GetId(IdCidade);
            if (cidade == null)
            {
                return NotFound("Cidade Não encontrada para o Id informado.");
            }
            var cidadeDTO = _mapper.Map<CidadeDTO>(cidade);
            return Ok(cidadeDTO);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CidadeRepository>>> GetCidade()
        {
            var cidade = await _cidadeRepository.GetAll();
            var cidadeDTO = _mapper.Map<IEnumerable<CidadeDTO>>(cidade);
            return Ok(cidadeDTO);
        }
    }
}
