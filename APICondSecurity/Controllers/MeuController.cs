/*namespace APICondSecurity.Controllers
{
    public class MeuController
    {
    }
}*/

using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class MeuController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Minha primeira API em C#!");
    }
}

