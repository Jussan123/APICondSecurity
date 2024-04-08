using APICondSecurity.DTOs;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioController(UsuarioRepository usuarioRepository, IMapper mapper)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarUsuario(Usuario usuario)
        {
            _usuarioRepository.Incluir(usuario);
            try
            {
                await _usuarioRepository.SaveAllAsync();
                return Ok("Usuario cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a usuario: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateUsuario(Usuario usuario)
        {
            _usuarioRepository.Alterar(usuario);
            try
            {
                await _usuarioRepository.SaveAllAsync();
                return Ok("Usuario alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a usuario: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdUsuario)
        {
            var usuario = _usuarioRepository.Get(IdUsuario);
            if (usuario == null)
            {
                return NotFound("Id da usuario não encontrado.");
            }
            _usuarioRepository.Excluir(await usuario);
            try
            {
                await _usuarioRepository.SaveAllAsync();
                return Ok("Usuario excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a usuario: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<UsuarioRepository>> Get(int IdUsuario)
        {
            var usuario = await _usuarioRepository.Get(IdUsuario);
            if (usuario == null)
            {
                return NotFound("Usuario Não encontrada para o Id informado.");
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UsuarioRepository>>> GetUsuario()
        {
            return Ok(await _usuarioRepository.GetAll());
        }
    }
}
