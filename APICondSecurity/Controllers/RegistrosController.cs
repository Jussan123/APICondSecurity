using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosController : Controller
    {
        private readonly RegistrosRepository _registrosRepository;
        public RegistrosController(RegistrosRepository registrosRepository)
        {
            _registrosRepository = registrosRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarRegistros(Registros registros)
        {
            _registrosRepository.Incluir(registros);
            try
            {
                await _registrosRepository.SaveAllAsync();
                return Ok("Registros cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a registros: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateRegistros(Registros registros)
        {
            _registrosRepository.Alterar(registros);
            try
            {
                await _registrosRepository.SaveAllAsync();
                return Ok("Registros alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a registros: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdRegistros)
        {
            var registros = _registrosRepository.Get(IdRegistros);
            if (registros == null)
            {
                return NotFound("Id da registros não encontrado.");
            }
            _registrosRepository.Excluir(await registros);
            try
            {
                await _registrosRepository.SaveAllAsync();
                return Ok("Registros excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a registros: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<RegistrosRepository>> Get(int IdRegistros)
        {
            var registros = await _registrosRepository.Get(IdRegistros);
            if (registros == null)
            {
                return NotFound("Registros Não encontrada para o Id informado.");
            }
            return Ok(registros);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RegistrosRepository>>> GetRegistros()
        {
            return Ok(await _registrosRepository.GetAll());
        }
    }
}
