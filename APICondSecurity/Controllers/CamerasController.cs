using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CamerasController : Controller
    {
        private readonly CameraRepository _cameraRepository;
        public CamerasController(CameraRepository cameraRepository)
        {
            _cameraRepository = cameraRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CameraRepository>>> GetCameras()
        {
            return Ok(await _cameraRepository.GetAll());
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

        

    }
}
