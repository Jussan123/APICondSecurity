using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface ITipoUsuario
    {
        void Incluir(TipoUsuario tipoUsuario);
        void Alterar(TipoUsuario tipoUsuario);
        void Excluir(TipoUsuario tipoUsuario);
        Task<TipoUsuario> Get(int id);
        Task<IEnumerable<TipoUsuario>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
