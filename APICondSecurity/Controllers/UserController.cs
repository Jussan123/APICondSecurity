using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Services;
using APICondSecurity.Identity;
using Microsoft.AspNetCore.Mvc;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using System.Text;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserRepository _userService;
        private readonly IMapper _mapper;

        public UserController(UserRepository userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Incluir(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var emailExiste = await _userService.UserExists(userDTO.Email);
            if (emailExiste)
            {
                return BadRequest("Este E-mail já possui um cadastro em nosso sistema.");
            }

            var user = _mapper.Map<User>(userDTO);

            user.SenhaSalt = [10];
            user.SenhaHash = Encoding.ASCII.GetBytes(userDTO.Senha);
            user.CpfSalt = [10];
            user.CpfHash = Encoding.ASCII.GetBytes(userDTO.Cpf);

            var userN = await _userService.Incluir(user);
            if (userN == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar o usuário.");
            }

            var token = _userService.GenerateToken(userN.Id_user, userN.Email);
            return new UserToken
            {
                Token = token
            };
        }

        [HttpPost("loginApp")]
        public async Task<ActionResult<UserToken>> LoginApp(UserLoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Senha))
            {
                return BadRequest("Email e Senha são obrigatórios.");
            }

            var user = await _userService.Login(loginDTO.Email, loginDTO.Senha);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            var token = _userService.GenerateToken(user.Id_user, user.Email);
            return new UserToken
            {
                Token = token
            };
        }
        /*
        [HttpGet("loginAplicacoes")]
        public async Task<bool> LoginAplicacoes(string email, string senha, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(token))
            {
                return false;
            }

            var user = await _userService.LoginAplicacoes(email, senha, token);
            if (user == null)
            {
                return false;
            }

            return true;
        } */
    }
}
