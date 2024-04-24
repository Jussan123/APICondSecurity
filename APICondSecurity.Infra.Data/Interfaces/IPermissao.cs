
using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IPermissao
    {
        void Incluir(Permissao permissao);
        void Alterar(Permissao permissao);
        void Excluir(Permissao permissao);
        Task<Permissao> Get(int id);
        Task<IEnumerable<Permissao>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
