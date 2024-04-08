using APICondSecurity.DTOs;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacaoController : Controller
    {
        private readonly NotificacaoRepository _notificacaoRepository;
        private readonly IMapper _mapper;
        public NotificacaoController(NotificacaoRepository notificacaoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _notificacaoRepository = notificacaoRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarNotificacao(Notificacao notificacao)
        {
            _notificacaoRepository.Incluir(notificacao);
            try
            {
                await _notificacaoRepository.SaveAllAsync();
                return Ok("Notificacao cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a notificacao: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateNotificacao(Notificacao notificacao)
        {
            _notificacaoRepository.Alterar(notificacao);
            try
            {
                await _notificacaoRepository.SaveAllAsync();
                return Ok("Notificacao alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a notificacao: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdNotificacao)
        {
            var notificacao = _notificacaoRepository.Get(IdNotificacao);
            if (notificacao == null)
            {
                return NotFound("Id da notificacao não encontrado.");
            }
            _notificacaoRepository.Excluir(await notificacao);
            try
            {
                await _notificacaoRepository.SaveAllAsync();
                return Ok("Notificacao excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a notificacao: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<NotificacaoRepository>> Get(int IdNotificacao)
        {
            var notificacao = await _notificacaoRepository.Get(IdNotificacao);
            if (notificacao == null)
            {
                return NotFound("Notificacao Não encontrada para o Id informado.");
            }
            var notificacaoDTO = _mapper.Map<NotificacaoDTO>(notificacao);
            return Ok(notificacao);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<NotificacaoRepository>>> GetNotificacao()
        {
            return Ok(await _notificacaoRepository.GetAll());
        }
    }
}
