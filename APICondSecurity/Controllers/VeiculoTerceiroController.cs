using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
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
        public async Task<ActionResult> CadastrarVeiculoTerceiro(VeiculoTerceiroDTO veiculoTerceiroDTO)
        {
            var veiculoTerceiro = _mapper.Map<VeiculoTerceiro>(veiculoTerceiroDTO);
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
        public async Task<ActionResult> UpdateVeiculoTerceiro(VeiculoTerceiroDTO veiculoTerceiroDTO)
        {
            if (veiculoTerceiroDTO.IdVeiculoTerceiro == null)
            {
                return BadRequest("Não é possível alterar o veículo. É necessário informar o Id.");
            }
            var veiculoTerceiroExiste = await _veiculoTerceiroRepository.Get(veiculoTerceiroDTO.IdVeiculoTerceiro);
            if (veiculoTerceiroExiste == null)
            {
                return NotFound("Veiculo Terceiro Não identificado.");
            }
            veiculoTerceiroExiste.Placa = veiculoTerceiroDTO.Placa;

            try
            {
                _veiculoTerceiroRepository.Alterar(veiculoTerceiroExiste);
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
