using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface IRegistros
    {
        void Incluir(Registros registros);
        void Alterar(Registros registros);
        void Excluir(Registros registros);
        Task<Registros> Get(int id);
        Task<IEnumerable<Registros>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
