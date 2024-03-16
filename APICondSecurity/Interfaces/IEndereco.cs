using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
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
