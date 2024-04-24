using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
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
        public async Task<ActionResult> CadastrarNotificacao(NotificacaoDTO notificacaoDTO)
        {
            var notificacao = _mapper.Map<Notificacao>(notificacaoDTO);
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
        /* Não vamos permitir alterar as notificações
         
        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateNotificacao(NotificacaoDTO notificacaoDTO)
        {
            if (notificacaoDTO.IdNotificacao == null)
            {
                return BadRequest("Não é Possivel alterar a notificação. É necessário informar od Id.");
            }
            var notificaoExiste = await _notificacaoRepository.Get(notificacaoDTO.IdNotificacao);
            if (notificaoExiste == null)
            {
                return NotFound("Notificação não encontrada.");
            }
            notificaoExiste.Situacao = notificacaoDTO.Situacao;
            notificaoExiste.Permissao = notificacaoDTO.Permissao;
            notificaoExiste.
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
        */

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
