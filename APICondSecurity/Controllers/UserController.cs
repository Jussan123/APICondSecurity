using APICondSecurity.Models;
using APICondSecurity.Infra.Data.DTOs;
using APICondSecurity.Infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using APICondSecurity.Account;
using APICondSecurity.Infra.Data.Interfaces;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAuthenticate _authenticateService;
        private readonly IUserService _userService;

        public UserController(IAuthenticate authenticateService, IUserService userService)
        {
            _authenticateService = authenticateService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Incluir(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var emailExiste = await _authenticateService.UserExists(userDTO.Email);
            if (emailExiste) 
            {
                return BadRequest("Este E-mail já possui um cadastro em nosso sistema.");
            }
            
            var user = await _userService.Incluir(userDTO);
            if (user == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar o usuário.");
            }

            var token = _authenticateService.GenerateToken(userDTO.IdUser, userDTO.Email);
            return new UserToken
            {
                Token = token
            };
        }
    }
}
