using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : Controller
    {
        private readonly EnderecoRepository _enderecoRepository;
        public EnderecoController(EnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarEndereco(Endereco endereco)
        {
            _enderecoRepository.Incluir(endereco);
            try
            {
                await _enderecoRepository.SaveAllAsync();
                return Ok("Endereco cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a endereco: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateEndereco(Endereco endereco)
        {
            _enderecoRepository.Alterar(endereco);
            try
            {
                await _enderecoRepository.SaveAllAsync();
                return Ok("Endereco alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a endereco: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdEndereco)
        {
            var endereco = _enderecoRepository.Get(IdEndereco);
            if (endereco == null)
            {
                return NotFound("Id da endereco não encontrado.");
            }
            _enderecoRepository.Excluir(await endereco);
            try
            {
                await _enderecoRepository.SaveAllAsync();
                return Ok("Endereco excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a endereco: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<EnderecoRepository>> Get(int IdEndereco)
        {
            var endereco = await _enderecoRepository.Get(IdEndereco);
            if (endereco == null)
            {
                return NotFound("Endereco Não encontrada para o Id informado.");
            }
            return Ok(endereco);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EnderecoRepository>>> GetEndereco()
        {
            return Ok(await _enderecoRepository.GetAll());
        }
    }
}
