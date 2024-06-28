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
    [Authorize]
    public class CamerasController(CameraRepository cameraRepository, IMapper mapper) : Controller
    {
        private readonly CameraRepository _cameraRepository = cameraRepository;
        private readonly IMapper _mapper = mapper;

        [HttpPost("Cadastrar")]
        [Authorize]
        public async Task<ActionResult> CadastrarCamera(CamerasDTO cameraDTO)
        {
            var camera = _mapper.Map<Cameras>(cameraDTO);
            _cameraRepository.Incluir(camera);
            try
            {
                await _cameraRepository.SaveAllAsync();
                return Ok("Câmera cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a câmera: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        [Authorize]
        public async Task<ActionResult> UpdateCamera(CamerasDTO cameraDTO)
        {
            if (cameraDTO.IdCamera == null)
            {
                return BadRequest("Não é possível alterar a camera é necessário informar o ID.");
            }

            var cameraExiste = await _cameraRepository.Get(cameraDTO.IdCamera);

            if (cameraExiste == null)
            {
                return NotFound("Camera não identificada.");
            }

            cameraExiste.IpCamera = cameraDTO.IpCamera;
            cameraExiste.Posicao = cameraDTO.Posicao;
            cameraExiste.Tipo = cameraDTO.Tipo;

            try
            {
                _cameraRepository.Alterar(cameraExiste);
                await _cameraRepository.SaveAllAsync();
                return Ok("Camera alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a camera: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<ActionResult> Delete(int IdCamera)
        {
            var camera = _cameraRepository.Get(IdCamera);
            if (camera == null)

            {
                return NotFound("Id da camera não encontrado.");
            }
            _cameraRepository.Excluir(await camera);
            try
            {
                await _cameraRepository.SaveAllAsync();
                return Ok("Camera excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a camera: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<ActionResult<CameraRepository>> Get(int IdCamera)
        {
            var camera = await _cameraRepository.Get(IdCamera);
            if (camera == null)
            {
                return NotFound("Camera Não encontrada para o Id informado.");
            }
            var cameraDTO = _mapper.Map<CamerasDTO>(camera);
            return Ok(cameraDTO);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CameraRepository>>> GetCameras()
        {
            var camera = await _cameraRepository.GetAll();
            var cameraDTO = _mapper.Map<IEnumerable<CamerasDTO>>(camera);
            return Ok(cameraDTO);
        }
    }
}
