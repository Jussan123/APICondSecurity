using APICondSecurity.DTOs;
using APICondSecurity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost("Registro")]
        public async Task<ActionResult<UserToken>> Incluir(UsuarioDTO usuarioDTO)
        {
            return View();
        }
    }
}
