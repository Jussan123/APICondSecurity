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
    public class VeiculoController : Controller
    {
        private readonly VeiculoRepository _veiculoRepository;
        private readonly IMapper _mapper;

        public VeiculoController(VeiculoRepository veiculoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _veiculoRepository = veiculoRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarVeiculo(Veiculo veiculo)
        {
            _veiculoRepository.Incluir(veiculo);
            try
            {
                await _veiculoRepository.SaveAllAsync();
                return Ok("Veiculo cadastrada com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao salvar a veiculo: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateVeiculo(Veiculo veiculo)
        {
            _veiculoRepository.Alterar(veiculo);
            try
            {
                await _veiculoRepository.SaveAllAsync();
                return Ok("Veiculo alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a veiculo: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdVeiculo)
        {
            var veiculo = _veiculoRepository.Get(IdVeiculo);
            if (veiculo == null)
            {
                return NotFound("Id da veiculo não encontrado.");
            }
            _veiculoRepository.Excluir(await veiculo);
            try
            {
                await _veiculoRepository.SaveAllAsync();
                return Ok("Veiculo excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a veiculo: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<VeiculoRepository>> Get(int IdVeiculo)
        {
            var veiculo = await _veiculoRepository.Get(IdVeiculo);
            if (veiculo == null)
            {
                return NotFound("Veiculo Não encontrada para o Id informado.");
            }

            var veiculoDTO = _mapper.Map<VeiculoDTO>(veiculo);
            return Ok(veiculoDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<VeiculoRepository>>> GetVeiculo()
        {
            return Ok(await _veiculoRepository.GetAll());
        }
    }
}
