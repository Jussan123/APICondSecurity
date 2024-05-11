using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IUsuario
    {
        Task<Usuario> Incluir(Usuario usuario);
        Task<Usuario> Alterar(Usuario usuario);
        Task<Usuario> Excluir(int idUsuario);
        Task<Usuario> ExcluirUser(Usuario usuario);
        Task<Usuario> Get(int id);
        Task<IEnumerable<Usuario>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
