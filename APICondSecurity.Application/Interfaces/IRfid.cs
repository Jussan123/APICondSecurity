using APICondSecurity.Domain.Models;

namespace APICondSecurity.Domain.Interfaces
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
