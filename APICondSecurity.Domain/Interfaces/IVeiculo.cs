using APICondSecurity.Domain.Models;

namespace APICondSecurity.Domain.Interfaces
{
    public interface IVeiculo
    {
        void Incluir(Veiculo veiculo);
        void Alterar(Veiculo veiculo);
        void Excluir(Veiculo veiculo);
        Task<Veiculo> Get(int id);
        Task<Veiculo> GetByPlaca(string placa);
        Task<IEnumerable<Veiculo>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
