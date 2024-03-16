using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UfController : Controller
    {
        private readonly UfRepository _ufRepository;
        public UfController(UfRepository ufRepository)
        {
            _ufRepository = ufRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarUf(Uf uf)
        {
            _ufRepository.Incluir(uf);
            try
            {
                await _ufRepository.SaveAllAsync();
                return Ok("Uf cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a uf: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateUf(Uf uf)
        {
            _ufRepository.Alterar(uf);
            try
            {
                await _ufRepository.SaveAllAsync();
                return Ok("Uf alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a uf: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdUf)
        {
            var uf = _ufRepository.Get(IdUf);
            if (uf == null)
            {
                return NotFound("Id da uf não encontrado.");
            }
            _ufRepository.Excluir(await uf);
            try
            {
                await _ufRepository.SaveAllAsync();
                return Ok("Uf excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a uf: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<UfRepository>> Get(int IdUf)
        {
            var uf = await _ufRepository.Get(IdUf);
            if (uf == null)
            {
                return NotFound("Uf Não encontrada para o Id informado.");
            }
            return Ok(uf);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UfRepository>>> GetUf()
        {
            return Ok(await _ufRepository.GetAll());
        }
    }
}
