using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoUsuarioController : Controller
    {
        private readonly VeiculoUsuarioRepository _veiculoUsuarioRepository;
        public VeiculoUsuarioController(VeiculoUsuarioRepository veiculoUsuarioRepository)
        {
            _veiculoUsuarioRepository = veiculoUsuarioRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarVeiculoUsuario(VeiculoUsuario veiculoUsuario)
        {
            _veiculoUsuarioRepository.Incluir(veiculoUsuario);
            try
            {
                await _veiculoUsuarioRepository.SaveAllAsync();
                return Ok("VeiculoUsuario cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a veiculoUsuario: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateVeiculoUsuario(VeiculoUsuario veiculoUsuario)
        {
            _veiculoUsuarioRepository.Alterar(veiculoUsuario);
            try
            {
                await _veiculoUsuarioRepository.SaveAllAsync();
                return Ok("VeiculoUsuario alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a veiculoUsuario: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdVeiculoUsuario)
        {
            var veiculoUsuario = _veiculoUsuarioRepository.Get(IdVeiculoUsuario);
            if (veiculoUsuario == null)
            {
                return NotFound("Id da veiculoUsuario não encontrado.");
            }
            _veiculoUsuarioRepository.Excluir(await veiculoUsuario);
            try
            {
                await _veiculoUsuarioRepository.SaveAllAsync();
                return Ok("VeiculoUsuario excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a veiculoUsuario: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<VeiculoUsuarioRepository>> Get(int IdVeiculoUsuario)
        {
            var veiculoUsuario = await _veiculoUsuarioRepository.Get(IdVeiculoUsuario);
            if (veiculoUsuario == null)
            {
                return NotFound("VeiculoUsuario Não encontrada para o Id informado.");
            }
            return Ok(veiculoUsuario);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<VeiculoUsuarioRepository>>> GetVeiculoUsuario()
        {
            return Ok(await _veiculoUsuarioRepository.GetAll());
        }
    }
}
