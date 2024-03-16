using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface ICamera
    {
        void Incluir(Cameras camera);
        void Alterar(Cameras camera);
        void Excluir(Cameras camera);
        Task<Cameras> Get(int id);
        Task<IEnumerable<Cameras>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
