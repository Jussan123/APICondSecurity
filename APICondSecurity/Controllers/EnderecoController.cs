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
    public class EnderecoController : Controller
    {
        private readonly EnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;
        public EnderecoController(EnderecoRepository enderecoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _enderecoRepository = enderecoRepository;
        }

        [HttpPost("Cadastrar")]
        [Authorize]
        public async Task<ActionResult> CadastrarEndereco(EnderecoDTO enderecoDTO)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDTO);
            _enderecoRepository.Incluir(endereco);
            try
            {
                await _enderecoRepository.SaveAllAsync();
                return Ok("Endereco cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar a endereco: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        [Authorize]
        public async Task<ActionResult> UpdateEndereco(EnderecoDTO enderecoDTO)
        {
            if (enderecoDTO.IdEndereco == null)
            {
                return BadRequest("Não é possível alterar o endereço. É necessário informar o ID.");
            }
            var enderecoExiste = await _enderecoRepository.Get(enderecoDTO.IdEndereco);
            if (enderecoExiste == null)
            {
                return NotFound("Endereço não encontrado.");
            }
            enderecoExiste.Rua = enderecoDTO.Rua;
            enderecoExiste.Numero = enderecoDTO.Numero;
            enderecoExiste.Bairro = enderecoDTO.Bairro;
            enderecoExiste.Cep = enderecoDTO.Cep;
            enderecoExiste.Complemento = enderecoDTO.Complemento;

            try
            {
                _enderecoRepository.Alterar(enderecoExiste);
                await _enderecoRepository.SaveAllAsync();
                return Ok("Endereco alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a endereco: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<ActionResult> Delete(int IdEndereco)
        {
            var endereco = _enderecoRepository.Get(IdEndereco);
            if (endereco == null)
            {
                return NotFound("Id da endereco não encontrado.");
            }
            _enderecoRepository.Excluir(await endereco);
            try
            {
                await _enderecoRepository.SaveAllAsync();
                return Ok("Endereco excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao excluir a endereco: {ex.Message}");
            }
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<ActionResult<EnderecoRepository>> Get(int IdEndereco)
        {
            var endereco = await _enderecoRepository.Get(IdEndereco);
            if (endereco == null)
            {
                return NotFound("Endereco Não encontrada para o Id informado.");
            }
            var enderecoDTO = _mapper.Map<EnderecoDTO>(endereco);
            return Ok(enderecoDTO);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EnderecoRepository>>> GetEndereco()
        {
            return Ok(await _enderecoRepository.GetAll());
        }
    }
}
