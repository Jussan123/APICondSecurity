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
    public class UfController : Controller
    {
        private readonly UfRepository _ufRepository;
        private readonly IMapper _mapper;
        public UfController(UfRepository ufRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ufRepository = ufRepository;
        }

        [HttpPost("Cadastrar")]
        [Authorize]
        public async Task<ActionResult> CadastrarUf(UfDTO ufDTO)
        {
            var uf = _mapper.Map<Uf>(ufDTO);
            _ufRepository.Incluir(uf);
            try
            {
                await _ufRepository.SaveAllAsync();
                return Ok("Uf cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a uf: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        [Authorize]
        public async Task<ActionResult> UpdateUf(UfDTO ufDTO)
        {
            if (ufDTO.IdUf == null)
            {
                return BadRequest("Não é possível alterar a Uf. É necessário informar o Id.");
            }
            var ufExiste = await _ufRepository.Get(ufDTO.IdUf);
            if (ufExiste == null)
            {
                return NotFound("Uf Não encontrada.");
            }

            ufExiste.Sigla = ufDTO.Sigla;
            ufExiste.Nome = ufDTO.Nome;
            try
            {
                _ufRepository.Alterar(ufExiste);
                await _ufRepository.SaveAllAsync();
                return Ok("Uf alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a uf: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<ActionResult> Delete(int IdUf)
        {
            var uf = _ufRepository.Get(IdUf);
            if (uf == null)
            {
                return NotFound("Id da uf não encontrado.");
            }
            _ufRepository.Excluir(await uf);
            try
            {
                await _ufRepository.SaveAllAsync();
                return Ok("Uf excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a uf: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<ActionResult<UfRepository>> Get(int IdUf)
        {
            var uf = await _ufRepository.Get(IdUf);
            if (uf == null)
            {
                return NotFound("Uf Não encontrada para o Id informado.");
            }
            var ufDTO = _mapper.Map<UfDTO>(uf);
            return Ok(ufDTO);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UfRepository>>> GetUf()
        {
            return Ok(await _ufRepository.GetAll());
        }
    }
}
