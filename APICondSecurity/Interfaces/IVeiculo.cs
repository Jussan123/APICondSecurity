using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface IVeiculo
    {
        void Incluir(Veiculo veiculo);
        void Alterar(Veiculo veiculo);
        void Excluir(Veiculo veiculo);
        Task<Veiculo> Get(int id);
        Task<IEnumerable<Veiculo>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
