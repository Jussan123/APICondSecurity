﻿using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidenciaController : Controller
    {
        private readonly ResidenciaRepository _residenciaRepository;
        public ResidenciaController(ResidenciaRepository residenciaRepository)
        {
            _residenciaRepository = residenciaRepository;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> CadastrarResidencia(Residencia residencia)
        {
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
        public async Task<ActionResult> UpdateResidencia(Residencia residencia)
        {
            _residenciaRepository.Alterar(residencia);
            try
            {
                await _residenciaRepository.SaveAllAsync();
                return Ok("Residencia alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a residencia: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
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
        public async Task<ActionResult<ResidenciaRepository>> Get(int IdResidencia)
        {
            var residencia = await _residenciaRepository.Get(IdResidencia);
            if (residencia == null)
            {
                return NotFound("Residencia Não encontrada para o Id informado.");
            }
            return Ok(residencia);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ResidenciaRepository>>> GetResidencia()
        {
            return Ok(await _residenciaRepository.GetAll());
        }
    }
}