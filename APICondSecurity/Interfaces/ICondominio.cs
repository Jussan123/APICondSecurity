using APICondSecurity.Models;

namespace APICondSecurity.Interfaces
{
    public interface ICondominio
    {
        void Alterar(Condominio condominio);
        void Incluir(Condominio condominio);
        void Excluir(Condominio condominio);
        Task<Condominio> Get(int id);
        Task<IEnumerable<Condominio>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
