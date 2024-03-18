using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface IPermissao
    {
        void Incluir(Permissao permissao);
        void Alterar(Permissao permissao);
        void Excluir(Permissao permissao);
        Task<Permissao> Get(int id);
        Task<IEnumerable<Permissao>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
