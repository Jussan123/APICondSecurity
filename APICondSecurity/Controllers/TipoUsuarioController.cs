using APICondSecurity.Application.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoUsuarioController : Controller
    {
        private readonly TipoUsuarioRepository _tipoUsuarioRepository;
        private readonly IMapper _mapper;
        public TipoUsuarioController(TipoUsuarioRepository tipoUsuarioRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarTipoUsuario(TipoUsuarioDTO tipoUsuarioDTO)
        {
            var tipoUsuario = _mapper.Map<TipoUsuario>(tipoUsuarioDTO);
            _tipoUsuarioRepository.Incluir(tipoUsuario);
            try
            {
                await _tipoUsuarioRepository.SaveAllAsync();
                return Ok("TipoUsuario cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a tipoUsuario: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateTipoUsuario(TipoUsuarioDTO tipoUsuarioDTO)
        {
            if (tipoUsuarioDTO.IdTipoUsuario == null)
            {
                return BadRequest("Não é possivel alterar o Tipo de Usuário. É necessário Informar o Id.");
            }

            var tipoUsuarioExiste = await _tipoUsuarioRepository.Get(tipoUsuarioDTO.IdTipoUsuario);
            if (tipoUsuarioExiste == null)
            {
                return NotFound("Tipo de usuário não encontrado.");
            }

            tipoUsuarioExiste.Tipo = tipoUsuarioDTO.Tipo;

            try
            {
                _tipoUsuarioRepository.Alterar(tipoUsuarioExiste);
                await _tipoUsuarioRepository.SaveAllAsync();
                return Ok("TipoUsuario alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a tipoUsuario: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdTipoUsuario)
        {
            var tipoUsuario = _tipoUsuarioRepository.Get(IdTipoUsuario);
            if (tipoUsuario == null)
            {
                return NotFound("Id da tipoUsuario não encontrado.");
            }
            _tipoUsuarioRepository.Excluir(await tipoUsuario);
            try
            {
                await _tipoUsuarioRepository.SaveAllAsync();
                return Ok("TipoUsuario excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a tipoUsuario: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<TipoUsuarioRepository>> Get(int IdTipoUsuario)
        {
            var tipoUsuario = await _tipoUsuarioRepository.Get(IdTipoUsuario);
            if (tipoUsuario == null)
            {
                return NotFound("TipoUsuario Não encontrada para o Id informado.");
            }
            var tipoUsuarioDTO = _mapper.Map<TipoUsuarioDTO>(tipoUsuario);
            return Ok(tipoUsuarioDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TipoUsuarioRepository>>> GetTipoUsuario()
        {
            return Ok(await _tipoUsuarioRepository.GetAll());
        }
    }
}
