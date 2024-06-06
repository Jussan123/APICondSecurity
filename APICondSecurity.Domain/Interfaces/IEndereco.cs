using APICondSecurity.Domain.Models;

namespace APICondSecurity.Domain.Interfaces
{
    public interface IEndereco
    {
        void Incluir(Endereco endereco);
        void Alterar(Endereco endereco);
        void Excluir(Endereco endereco);
        Task<Endereco> Get(int id);
        Task<IEnumerable<Endereco>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
