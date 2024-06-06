using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
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
