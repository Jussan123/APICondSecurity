using APICondSecurity.DTOs;

namespace APICondSecurity.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Incluir(UserDTO userDTO);
        Task<UserDTO> Alterar(UserDTO userDTO);
        Task<UserDTO> Excluir(int idUser);
        Task<UserDTO> Get(int id);
        Task<UserDTO> Login(string Email, string Senha);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
