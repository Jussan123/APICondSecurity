using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
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
        public async Task<ActionResult> CadastrarUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDTO);
                await _usuarioRepository.Incluir(usuario);
                await _usuarioRepository.SaveAllAsync();
                return Ok("Usuario cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar o usuario: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO.IdUsuario == null)
            {
                return BadRequest("Não foi possível alterar usuário. è necessário informar o Id.");
            }
            var usuarioExiste = await _usuarioRepository.Get(usuarioDTO.IdUsuario);
            if (usuarioExiste == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            usuarioExiste.Nome = usuarioDTO.Nome;
            usuarioExiste.Telefone = usuarioDTO.Telefone;
            usuarioExiste.Email = usuarioDTO.Email;
            usuarioExiste.Senha = usuarioDTO.Senha;
            usuarioExiste.Situacao = usuarioDTO.Situacao;

            try
            {
                _usuarioRepository.Alterar(usuarioExiste);
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
            if (usuario != null)
            {
                _usuarioRepository.ExcluirUser(await usuario);
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
            return NotFound("Id da usuario não encontrado.");
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
