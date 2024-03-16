using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
