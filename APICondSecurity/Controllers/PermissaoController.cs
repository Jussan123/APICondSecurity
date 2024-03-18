using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissaoController : Controller
    {
        private readonly PermissaoRepository _permissaoRepository;
        public PermissaoController(PermissaoRepository permissaoRepository)
        {
            _permissaoRepository = permissaoRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarPermissao(Permissao permissao)
        {
            _permissaoRepository.Incluir(permissao);
            try
            {
                await _permissaoRepository.SaveAllAsync();
                return Ok("Permissao cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a permissao: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdatePermissao(Permissao permissao)
        {
            _permissaoRepository.Alterar(permissao);
            try
            {
                await _permissaoRepository.SaveAllAsync();
                return Ok("Permissao alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a permissao: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdPermissao)
        {
            var permissao = _permissaoRepository.Get(IdPermissao);
            if (permissao == null)
            {
                return NotFound("Id da permissao não encontrado.");
            }
            _permissaoRepository.Excluir(await permissao);
            try
            {
                await _permissaoRepository.SaveAllAsync();
                return Ok("Permissao excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a permissao: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<PermissaoRepository>> Get(int IdPermissao)
        {
            var permissao = await _permissaoRepository.Get(IdPermissao);
            if (permissao == null)
            {
                return NotFound("Permissao Não encontrada para o Id informado.");
            }
            return Ok(permissao);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PermissaoRepository>>> GetPermissao()
        {
            return Ok(await _permissaoRepository.GetAll());
        }
    }
}
