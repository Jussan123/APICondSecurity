using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface INotificacao
    {
        void Incluir(Notificacao notificacao);
        void Alterar(Notificacao notificacao);
        void Excluir(Notificacao notificacao);
        Task<Notificacao> Get(int id);
        Task<IEnumerable<Notificacao>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
