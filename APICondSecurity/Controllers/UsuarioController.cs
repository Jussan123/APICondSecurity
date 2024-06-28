using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsuarioController(UserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost("Cadastrar")]
        [Authorize]
        public async Task<ActionResult> CadastrarUser(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                await _userRepository.Incluir(user);
                await _userRepository.SaveAllAsync();
                return Ok("User cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar o user: {ex.Message}");
            }
        }

        [HttpPut("Alterar")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(UserDTO userDTO)
        {
            if (userDTO.IdUser == null)
            {
                return BadRequest("Não foi possível alterar usuário. è necessário informar o Id.");
            }
            var userExiste = await _userRepository.Get(userDTO.IdUser);
            if (userExiste == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            using var hmac = new HMACSHA512();
            byte[] SenhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Senha));
            byte[] SenhaSalt = hmac.Key;
            byte[] CpfHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Cpf));
            byte[] CpfSalt = hmac.Key;


            userExiste.Name = userDTO.Name;
            userExiste.Telefone = userDTO.Telefone;
            userExiste.Email = userDTO.Email;
            userExiste.Situacao = userDTO.Situacao;
            userExiste.IdTipoUsuario = userDTO.IdTipoUsuario;
            userExiste.Cpf = userDTO.Cpf;

            try
            {
                await _userRepository.Alterar(userExiste);
                await _userRepository.SaveAllAsync();
                return Ok("User alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar a user: {ex.Message}");
            }
        }

        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<ActionResult> Delete(int IdUser)
        {
            var user = _userRepository.Get(IdUser);
            if (user != null)
            {
                _userRepository.ExcluirUser(await user);
                try
                {
                    await _userRepository.SaveAllAsync();
                    return Ok("User excluída com sucesso.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Ocorreu um erro ao excluir a user: {ex.Message}");
                }
            }
            return NotFound("Id da user não encontrado.");
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<ActionResult<UserRepository>> Get(int IdUser)
        {
            var user = await _userRepository.Get(IdUser);
            if (user == null)
            {
                return NotFound("User não encontrada para o id informado.");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserRepository>>> GetUser()
        {
            return Ok(await _userRepository.GetAll());
        }
    }
}
