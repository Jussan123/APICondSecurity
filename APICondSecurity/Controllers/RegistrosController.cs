using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosController(RegistrosRepository registrosRepository, IMapper mapper) : Controller, IRegistrosController
    {
        private readonly RegistrosRepository _registrosRepository = registrosRepository;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly VeiculoRepository _veiculoRepository;
        private readonly PortaoRepository _portaoRepository;
        private readonly IMapper _mapper = mapper;

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarRegistros(RegistrosDTO registrosDTO)
        {
            var registros = _mapper.Map<Registros>(registrosDTO);
            _registrosRepository.Incluir(registros);
            try
            {
                await _registrosRepository.SaveAllAsync();
                return Ok("Registros cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a registros: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateRegistros(RegistrosDTO registrosDTO)
        {
            if (registrosDTO.IdRegistros == null)
            {
                return BadRequest("Não é possível alterar o Registro. É necessário informar o Id.");
            }
            var registrosExiste = await _registrosRepository.Get(registrosDTO.IdRegistros);
            if (registrosExiste == null)
            {
                return BadRequest("Registro não encontrado.");
            }

            var usuarioExiste = await _usuarioRepository.Get(registrosDTO.IdUsuario);
            if (usuarioExiste == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            // Verificar se o IdPortao existe
            var portaoExiste = await _portaoRepository.Get(registrosDTO.IdPortao);
            if (portaoExiste == null)
            {
                return BadRequest("Portão não encontrado.");
            }

            var placaExiste = await _veiculoRepository.GetByPlaca(registrosDTO.Placa);
            if (placaExiste == null)
            {
                return BadRequest("Placa não encontrada.");
            }

            registrosExiste.DataHoraEntrada = registrosDTO.DataHoraEntrada;
            registrosExiste.DataHoraSaida = registrosDTO.DataHoraSaida;
            registrosExiste.Placa = registrosDTO.Placa;
            registrosExiste.IdPortao = registrosDTO.IdPortao;
            registrosExiste.IdUsuario = registrosDTO.IdUsuario;

            try
            {
                _registrosRepository.Alterar(registrosExiste);
                await _registrosRepository.SaveAllAsync();
                return Ok("Registros alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a registros: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult> Delete(int IdRegistros)
        {
            var registros = _registrosRepository.Get(IdRegistros);
            if (registros == null)
            {
                return NotFound("Id da registros não encontrado.");
            }
            _registrosRepository.Excluir(await registros);
            try
            {
                await _registrosRepository.SaveAllAsync();
                return Ok("Registros excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a registros: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<RegistrosRepository>> Get(int IdRegistros)
        {
            var registros = await _registrosRepository.Get(IdRegistros);
            if (registros == null)
            {
                return NotFound("Registros Não encontrada para o Id informado.");
            }
            var registrosDTO = _mapper.Map<RegistrosDTO>(registros);
            return Ok(registrosDTO);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RegistrosRepository>>> GetRegistros()
        {
            return Ok(await _registrosRepository.GetAll());
        }
    }
}
