using APICondSecurity.Application.DTOs;
using APICondSecurity.Infra.Data.Repositories;
using APICondSecurity.Infra.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly CameraRepository _cameraRepository;
        public TokenController(CameraRepository cameraRepository)
        {
            _cameraRepository = cameraRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarCamera(Cameras camera)
        {
            _cameraRepository.Incluir(camera);
            if (!await _cameraRepository.SaveAllAsync())
            {
                return BadRequest("Ocorreu um erro ao salvar a câmera");
            }

            return Ok("Câmera cadastrada com sucesso!");
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> UpdateCamera(Cameras camera)
        {
            _cameraRepository.Alterar(camera);
            try
            {
                await _cameraRepository.SaveAllAsync();
                return Ok("Camera alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a camera: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
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
        public async Task<ActionResult<CameraRepository>> Get(int IdCamera)
        {
            var camera = await _cameraRepository.Get(IdCamera);
            if (camera == null)
            {
                return NotFound("Camera Não encontrada para o Id informado.");
            }
            return Ok(camera);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CameraRepository>>> GetCameras()
        {
            return Ok(await _cameraRepository.GetAll());
        }
    }
}
