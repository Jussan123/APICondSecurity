using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.DTOs;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> Incluir(UsuarioDTO usuarioDTO);
        Task<UsuarioDTO> Alterar(UsuarioDTO usuarioDTO);
        Task<UsuarioDTO> Excluir(int  idUsuario);
        Task<UsuarioDTO> Get(int id);
        Task<IEnumerable<UsuarioDTO>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
