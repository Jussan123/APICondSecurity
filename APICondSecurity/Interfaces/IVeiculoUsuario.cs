using APICondSecurity.Models;

namespace APICondSecurity.Interfaces
{
    public interface IVeiculoUsuario
    {
        void Incluir(VeiculoUsuario veiculoUsuario);
        void Alterar(VeiculoUsuario veiculoUsuario);
        void Excluir(VeiculoUsuario veiculoUsuario);
        Task<VeiculoUsuario> Get(int id);
        Task<IEnumerable<VeiculoUsuario>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
