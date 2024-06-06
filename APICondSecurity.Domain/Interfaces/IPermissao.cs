using APICondSecurity.Domain.Models;

namespace APICondSecurity.Domain.Interfaces
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
