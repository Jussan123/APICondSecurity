using APICondSecurity.Models;

namespace APICondSecurity.Interfaces
{
    public interface IVeiculoTerceiro
    {
        void Incluir(VeiculoTerceiro veiculoTerceiro);
        void Alterar(VeiculoTerceiro veiculoTerceiro);
        void Excluir(VeiculoTerceiro veiculoTerceiro);
        Task<VeiculoTerceiro> Get(int id);
        Task<IEnumerable<VeiculoTerceiro>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
