using APICondSecurity.Domain.Models;
using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface ICidade
    {
        void Incluir(Cidade cidade);
        void Alterar(Cidade cidade);
        void Excluir(Cidade cidade);
        Task<Cidade> Get(int id);

        Task<IEnumerable<Cidade>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
