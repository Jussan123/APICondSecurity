using APICondSecurity.Infra.Data.Models;


namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> Incluir(UsuarioDTO usuarioDTO);
        Task<UsuarioDTO> Alterar(UsuarioDTO usuarioDTO);
        Task<UsuarioDTO> Excluir(UsuarioDTO usuarioDTO);
        Task<UsuarioDTO> Get(int id);
        Task<IEnumerable<Usuario>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
