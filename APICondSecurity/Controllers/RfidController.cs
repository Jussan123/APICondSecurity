using APICondSecurity.DTOs;
using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RfidController : Controller
    {
        private readonly RfidRepository _rfidRepository;
        private readonly IMapper _mapper;
        public RfidController(RfidRepository rfidRepository, IMapper mapper)
        {
            _mapper = mapper;
            _rfidRepository = rfidRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarRfid(Rfid rfid)
        {
            _rfidRepository.Incluir(rfid);
            try
            {
                await _rfidRepository.SaveAllAsync();
                return Ok("Rfid cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a rfid: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateRfid(Rfid rfid)
        {
            _rfidRepository.Alterar(rfid);
            try
            {
                await _rfidRepository.SaveAllAsync();
                return Ok("Rfid alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a rfid: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdRfid)
        {
            var rfid = _rfidRepository.Get(IdRfid);
            if (rfid == null)
            {
                return NotFound("Id da rfid não encontrado.");
            }
            _rfidRepository.Excluir(await rfid);
            try
            {
                await _rfidRepository.SaveAllAsync();
                return Ok("Rfid excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a rfid: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<RfidRepository>> Get(int IdRfid)
        {
            var rfid = await _rfidRepository.Get(IdRfid);
            if (rfid == null)
            {
                return NotFound("Rfid Não encontrada para o Id informado.");
            }
            var rfidDTO = _mapper.Map<RfidDTO>(rfid);
            return Ok(rfidDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RfidRepository>>> GetRfid()
        {
            return Ok(await _rfidRepository.GetAll());
        }
    }
}
