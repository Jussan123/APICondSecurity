using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IUser
    {
        Task<User> Incluir(User user);
        Task<User> Alterar(User user);
        Task<User> Excluir(int idUser);
        Task<User> ExcluirUser(User user);
        Task<User> Get(int id);
        Task<IEnumerable<User>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
