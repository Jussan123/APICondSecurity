using APICondSecurity.Models;

namespace APICondSecurity.Interfaces
{
    public interface IUser
    {
        Task<User> Incluir(User user);
        Task<User> Alterar(User user);
        Task<bool> Excluir(int idUser);
        Task<bool> ExcluirUser(User user);
        Task<User> Get(int id);
        Task<IEnumerable<User>> GetAll();
        Task<bool> SaveAllAsync();
        Task<User> Login(string email, string senha);

    }
}
