using APICondSecurity.Models;

namespace APICondSecurity.Interfaces
{
    public interface IUf
    {
        void Incluir(Uf uf);
        void Alterar(Uf uf);
        void Excluir(Uf uf);
        Task<Uf> Get(int id);
        Task<IEnumerable<Uf>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
