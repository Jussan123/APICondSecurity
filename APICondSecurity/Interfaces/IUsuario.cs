using APICondSecurity.Models;

namespace APICondSecurity.Interfaces
{
    public interface IUsuario
    {
        void Incluir(Usuario usuario);
        void Alterar(Usuario usuario);
        void Excluir(Usuario usuario);
        Task<Usuario> Get(int id);
        Task<IEnumerable<Usuario>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
