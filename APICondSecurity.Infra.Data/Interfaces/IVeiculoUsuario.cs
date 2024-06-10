using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IVeiculoUsuario
    {
        void Incluir(VeiculoUsuario veiculoUsuario);
        void Alterar(VeiculoUsuario veiculoUsuario);
        void Excluir(VeiculoUsuario veiculoUsuario);
        Task<VeiculoUsuario> Get(int id);
        Task<VeiculoUsuario> GetByRfid(int IdRfid);
        Task<IEnumerable<VeiculoUsuario>> GetAll();
        Task<bool> SaveAllAsync();
        Task<VeiculoUsuario> GetByPlaca(string placa);
    }
}
