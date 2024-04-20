using APICondSecurity.Domain.Models;

namespace APICondSecurity.Domain.Interfaces
{
    public interface IPortao
    {
        void Incluir(Portao portao);
        void Alterar(Portao portao);
        void Excluir(Portao portao);
        Task<Portao> Get(int id);
        Task<IEnumerable<Portao>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
