using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface IRfid
    {
        void Incluir(Rfid rfid);
        void Alterar(Rfid rfid);
        void Excluir(Rfid rfid);
        Task<Rfid> Get(int id);
        Task<IEnumerable<Rfid>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
