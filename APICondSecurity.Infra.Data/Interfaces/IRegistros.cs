using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IRegistros
    {
        void Incluir(Registros registros);
        void Alterar(Registros registros);
        void Excluir(Registros registros);
        Task<Registros> Get(int id);
        Task<IEnumerable<Registros>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
