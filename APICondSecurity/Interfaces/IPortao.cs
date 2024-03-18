using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface IPortao
    {
        void Incluir(Portao portao);
        void Alterar(Portao portao);
        void Excluir(Portao portao);
        Task<Portao> Get(int id);
        Task<IEnumerable<Portao>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
