using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IUserService
    {
        Task<User> Incluir(User user);
        Task<User> Alterar(User user);
        Task<bool> Excluir(int idUser);
        Task<bool> ExcluirUser(User user);
        Task<User> Get(int id);
        Task<User> Login(string Email, string Senha);
        //Task<User> LoginAplicacoes(string Email, string Senha, string Token);
        Task<IEnumerable<User>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
