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
    public class ResidenciaController : Controller
    {
        private readonly ResidenciaRepository _residenciaRepository;
        private readonly IMapper _mapper;
        public ResidenciaController(ResidenciaRepository residenciaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _residenciaRepository = residenciaRepository;
        }

        [HttpPost("Cadastrar")]
        [Authorize]
        public async Task<ActionResult> CadastrarResidencia(ResidenciaDTO residenciaDTO)
        {
            var residencia = _mapper.Map<Residencia>(residenciaDTO);
            _residenciaRepository.Incluir(residencia);
            try
            {
                await _residenciaRepository.SaveAllAsync();
                return Ok("Residencia cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a residencia: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        [Authorize]
        public async Task<ActionResult> UpdateResidencia(ResidenciaDTO residenciaDTO)
        {
            if (residenciaDTO.IdResidencia == null)
            {
                return BadRequest("Não é possivel alterar a residência. É necessário o Id.");
            }
            var residenciaExiste = await _residenciaRepository.Get(residenciaDTO.IdResidencia);

            if (residenciaExiste == null)
            {
                return NotFound("Residência não encontrada.");
            }

            residenciaExiste.Numero = residenciaDTO.Numero;
            residenciaExiste.Bloco = residenciaDTO.Bloco;
            residenciaExiste.Quadra = residenciaDTO.Quadra;
            residenciaExiste.Rua = residenciaDTO.Rua;

            try
            {
                _residenciaRepository.Alterar(residenciaExiste);
                await _residenciaRepository.SaveAllAsync();
                return Ok("Residencia alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a residencia: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<ActionResult> Delete(int IdResidencia)
        {
            var residencia = _residenciaRepository.Get(IdResidencia);
            if (residencia == null)
            {
                return NotFound("Id da residencia não encontrado.");
            }
            _residenciaRepository.Excluir(await residencia);
            try
            {
                await _residenciaRepository.SaveAllAsync();
                return Ok("Residencia excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a residencia: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<ActionResult<ResidenciaRepository>> Get(int IdResidencia)
        {
            var residencia = await _residenciaRepository.Get(IdResidencia);
            if (residencia == null)
            {
                return NotFound("Residencia Não encontrada para o Id informado.");
            }
            var residenciaDTO = _mapper.Map<ResidenciaDTO>(residencia);
            return Ok(residenciaDTO);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ResidenciaRepository>>> GetResidencia()
        {
            return Ok(await _residenciaRepository.GetAll());
        }
    }
}
