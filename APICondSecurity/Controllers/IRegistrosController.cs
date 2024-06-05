using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICondSecurity.Controllers
{
    public interface IRegistrosController
    {
        Task<ActionResult> CadastrarRegistros(RegistrosDTO registrosDTO);
        Task<ActionResult> Delete(int IdRegistros);
        Task<ActionResult<RegistrosRepository>> Get(int IdRegistros);
        Task<ActionResult<IEnumerable<RegistrosRepository>>> GetRegistros();
        Task<ActionResult> UpdateRegistros(RegistrosDTO registrosDTO);
    }
}