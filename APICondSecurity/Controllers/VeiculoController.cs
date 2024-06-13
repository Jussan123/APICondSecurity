using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult> CadastrarVeiculo(VeiculoDTO veiculoDTO)
        {
            var veiculo = _mapper.Map<Veiculo>(veiculoDTO);
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
        [Authorize]
        public async Task<ActionResult> UpdateVeiculo(VeiculoDTO veiculoDTO)
        {
            if (veiculoDTO.IdVeiculo == null)
            {
                return BadRequest("não é possível alterar o veículo. É necessário informar o Id.");
            }
            var veiculoExiste = await _veiculoRepository.Get(veiculoDTO.IdVeiculo);
            if (veiculoExiste == null)
            {
                return NotFound("Veículo não encontrado");
            }
            veiculoExiste.Placa = veiculoDTO.Placa;
            veiculoExiste.Marca = veiculoDTO.Marca;
            veiculoExiste.Modelo = veiculoDTO.Modelo;
            veiculoExiste.Cor = veiculoDTO.Cor;
            veiculoExiste.Ano = veiculoDTO.Ano;
            veiculoExiste.Situacao = veiculoDTO.Situacao;

            try
            {
                _veiculoRepository.Alterar(veiculoExiste);
                await _veiculoRepository.SaveAllAsync();
                return Ok("Veiculo alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a veiculo: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<VeiculoRepository>>> GetVeiculo()
        {
            return Ok(await _veiculoRepository.GetAll());
        }
    }
}
