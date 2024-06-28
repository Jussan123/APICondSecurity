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
    public class VeiculoUsuarioController : Controller
    {
        private readonly VeiculoUsuarioRepository _veiculoUsuarioRepository;
        private readonly RfidRepository _rfidRepository;
        private readonly IMapper _mapper;
        public VeiculoUsuarioController(VeiculoUsuarioRepository veiculoUsuarioRepository, IMapper mapper, RfidRepository rfidRepository)
        {
            _mapper = mapper;
            _veiculoUsuarioRepository = veiculoUsuarioRepository;
            _rfidRepository = rfidRepository;
        }

        [HttpPost("Cadastrar")]
        [Authorize]
        public async Task<ActionResult> CadastrarVeiculoUsuario(VeiculoUsuarioDTO veiculoUsuarioDTO)
        {
            var veiculoUsuario = _mapper.Map<VeiculoUsuario>(veiculoUsuarioDTO);
            _veiculoUsuarioRepository.Incluir(veiculoUsuario);
            try
            {
                await _veiculoUsuarioRepository.SaveAllAsync();
                return Ok("VeiculoUsuario cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a veiculoUsuario: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        [Authorize]
        public async Task<ActionResult> UpdateVeiculoUsuario(VeiculoUsuarioDTO veiculoUsuarioDTO)
        {
            if (veiculoUsuarioDTO.IdVeiculoUsuario == null)
            {
                return BadRequest("Não é possível alterar o veículo do usuário. É necessário informar o Id.");
            }
            var veiculoUsuarioExiste = await _veiculoUsuarioRepository.Get(veiculoUsuarioDTO.IdVeiculoUsuario);
            if (veiculoUsuarioDTO == null)
            {
                return NotFound("Veiculo do Usuário não encontrado.");
            }
            var rfidExiste = await _rfidRepository.Get(veiculoUsuarioDTO.IdRfid);
            if (rfidExiste.IdRfid == null)
            {
                return BadRequest("RFID não encontrado.");
            }
            var rfidcadastrado = await _veiculoUsuarioRepository.GetByRfid(rfidExiste.IdRfid);
            if (rfidcadastrado.IdRfid != null)
            {
                return BadRequest("RFID já cadastrado para outro veículo");
            }

            veiculoUsuarioExiste.Placa = veiculoUsuarioDTO.Placa;
            veiculoUsuarioExiste.IdRfid = veiculoUsuarioDTO.IdRfid;
            try
            {
                _veiculoUsuarioRepository.Alterar(veiculoUsuarioExiste);
                await _veiculoUsuarioRepository.SaveAllAsync();
                return Ok("VeiculoUsuario alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a veiculoUsuario: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<ActionResult> Delete(int IdVeiculoUsuario)
        {
            var veiculoUsuario = _veiculoUsuarioRepository.Get(IdVeiculoUsuario);
            if (veiculoUsuario == null)
            {
                return NotFound("Id da veiculoUsuario não encontrado.");
            }
            _veiculoUsuarioRepository.Excluir(await veiculoUsuario);
            try
            {
                await _veiculoUsuarioRepository.SaveAllAsync();
                return Ok("VeiculoUsuario excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a veiculoUsuario: {ex.Message}");
            }
        }

        [HttpGet("GetPlaca")]
        [Authorize]
        public async Task<ActionResult<VeiculoUsuarioRepository>> GetPlaca(string placa)
        {
            var veiculoUsuario = await _veiculoUsuarioRepository.GetByPlaca(placa);
            if (veiculoUsuario == null)
            {
                return NotFound("VeiculoUsuario Não encontrada para o Id informado.");
            }
            var veiculoUsuarioDTO = _mapper.Map<VeiculoUsuarioDTO>(veiculoUsuario);
            return Ok(veiculoUsuarioDTO);
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<ActionResult<VeiculoUsuarioRepository>> Get(int IdVeiculoUsuario)
        {
            var veiculoUsuario = await _veiculoUsuarioRepository.Get(IdVeiculoUsuario);
            if (veiculoUsuario == null)
            {
                return NotFound("VeiculoUsuario Não encontrada para o Id informado.");
            }
            var veiculoUsuarioDTO = _mapper.Map<VeiculoUsuarioDTO>(veiculoUsuario);
            return Ok(veiculoUsuarioDTO);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VeiculoUsuarioRepository>>> GetVeiculoUsuario()
        {
            return Ok(await _veiculoUsuarioRepository.GetAll());
        }
    }
}
