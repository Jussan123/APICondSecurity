
using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
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
