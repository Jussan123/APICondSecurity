using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IResidencia
    {
        void Incluir(Residencia residencia);
        void Alterar(Residencia residencia);
        void Excluir(Residencia residencia);
        Task<Residencia> Get(int id);
        Task<IEnumerable<Residencia>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
