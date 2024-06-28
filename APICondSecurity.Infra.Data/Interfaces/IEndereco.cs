using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IEndereco
    {
        int Incluir(Endereco endereco);
        void Alterar(Endereco endereco);
        void Excluir(Endereco endereco);
        Task<Endereco> Get(int id);
        Task<IEnumerable<Endereco>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
