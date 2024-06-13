using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Services;
using APICondSecurity.Identity;
using Microsoft.AspNetCore.Mvc;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using System.Text;
using System.Security.Cryptography;

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
        //[Authorize]
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
            if(userDTO.Senha != null && userDTO.Cpf != null)
            {
                using var hmac = new HMACSHA512();
                byte[] SenhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Senha));
                byte[] SenhaSalt = hmac.Key;
                byte[] CpfHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Cpf));
                byte[] CpfSalt = hmac.Key;
                
                user.AlterarSenha(SenhaHash, SenhaSalt);
                user.CpfHash = CpfHash;
                user.CpfSalt = CpfSalt;
            }
            /*
            user.SenhaSalt = [10];
            user.SenhaHash = Encoding.ASCII.GetBytes(userDTO.Senha);
            user.CpfSalt = [10];
            user.CpfHash = Encoding.ASCII.GetBytes(userDTO.Cpf);*/

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
        public async Task<ActionResult<UserToken>> LoginApp(LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Senha))
            {
                return BadRequest("Email e Senha são obrigatórios.");
            }

            var emailExiste = await _userService.UserExists(loginModel.Email);
            if (!emailExiste)
            {
                return BadRequest("Usuário não encontrado.");
            }

            var senhaCorreta = await _userService.AuthenticateAsync(loginModel.Email, loginModel.Senha);
            if (!senhaCorreta)
            {
                return BadRequest("Senha incorreta.");
            }

            var user = await _userService.GetUserByEmail(loginModel.Email);

            var token = _userService.GenerateToken(user.Id_user, user.Email);
            return new UserToken
            {
                Token = token
            };
        }
        
    }
}
