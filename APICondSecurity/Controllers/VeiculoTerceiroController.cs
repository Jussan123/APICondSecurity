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
    public class VeiculoTerceiroController : Controller
    {
        private readonly VeiculoTerceiroRepository _veiculoTerceiroRepository;
        private readonly IMapper _mapper;

        public VeiculoTerceiroController(VeiculoTerceiroRepository veiculoTerceiroRepository, IMapper mapper)
        {
            _mapper = mapper;
            _veiculoTerceiroRepository = veiculoTerceiroRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarVeiculoTerceiro(VeiculoTerceiro veiculoTerceiro)
        {
            _veiculoTerceiroRepository.Incluir(veiculoTerceiro);
            try
            {
                await _veiculoTerceiroRepository.SaveAllAsync();
                return Ok("VeiculoTerceiro cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a veiculoTerceiro: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateVeiculoTerceiro(VeiculoTerceiro veiculoTerceiro)
        {
            _veiculoTerceiroRepository.Alterar(veiculoTerceiro);
            try
            {
                await _veiculoTerceiroRepository.SaveAllAsync();
                return Ok("VeiculoTerceiro alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a veiculoTerceiro: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdVeiculoTerceiro)
        {
            var veiculoTerceiro = _veiculoTerceiroRepository.Get(IdVeiculoTerceiro);
            if (veiculoTerceiro == null)
            {
                return NotFound("Id da veiculoTerceiro não encontrado.");
            }
            _veiculoTerceiroRepository.Excluir(await veiculoTerceiro);
            try
            {
                await _veiculoTerceiroRepository.SaveAllAsync();
                return Ok("VeiculoTerceiro excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a veiculoTerceiro: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<VeiculoTerceiroRepository>> Get(int IdVeiculoTerceiro)
        {
            var veiculoTerceiro = await _veiculoTerceiroRepository.Get(IdVeiculoTerceiro);
            if (veiculoTerceiro == null)
            {
                return NotFound("VeiculoTerceiro Não encontrada para o Id informado.");
            }
            var veiculoTerceiroDTO = _mapper.Map<VeiculoTerceiroDTO>(veiculoTerceiro);
            return Ok(veiculoTerceiroDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<VeiculoTerceiroRepository>>> GetVeiculoTerceiro()
        {
            return Ok(await _veiculoTerceiroRepository.GetAll());
        }
    }
}
