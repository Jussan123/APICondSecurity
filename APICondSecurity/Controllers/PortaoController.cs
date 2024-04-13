using APICondSecurity.DTOs;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortaoController : Controller
    {
        private readonly PortaoRepository _portaoRepository;
        private readonly IMapper _mapper;
        public PortaoController(PortaoRepository portaoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _portaoRepository = portaoRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarPortao(PortaoDTO portaoDTO)
        {
            var portao = _mapper.Map<Portao>(portaoDTO);
            _portaoRepository.Incluir(portao);
            try
            {
                await _portaoRepository.SaveAllAsync();
                return Ok("Portao cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a portao: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdatePortao(PortaoDTO portaoDTO)
        {
            if (portaoDTO.IdPortao == null) 
            {
                return BadRequest("Não é possivel alterar o portão. É necessário informar o Id.");
            }
            var portaoExiste = await _portaoRepository.Get(portaoDTO.IdPortao);
            if (portaoExiste == null)
            {
                return NotFound("Portão não encontrado.");
            }

            portaoExiste.Nome = portaoDTO.Nome;

            try
            {
                _portaoRepository.Alterar(portaoExiste);
                await _portaoRepository.SaveAllAsync();
                return Ok("Portao alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a portao: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdPortao)
        {
            var portao = _portaoRepository.Get(IdPortao);
            if (portao == null)
            {
                return NotFound("Id da portao não encontrado.");
            }
            _portaoRepository.Excluir(await portao);
            try
            {
                await _portaoRepository.SaveAllAsync();
                return Ok("Portao excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a portao: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<PortaoRepository>> Get(int IdPortao)
        {
            var portao = await _portaoRepository.Get(IdPortao);
            if (portao == null)
            {
                return NotFound("Portao Não encontrada para o Id informado.");
            }
            var portaoDTO = _mapper.Map<PortaoDTO>(portao);
            return Ok(portaoDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PortaoRepository>>> GetPortao()
        {
            return Ok(await _portaoRepository.GetAll());
        }
    }
}
