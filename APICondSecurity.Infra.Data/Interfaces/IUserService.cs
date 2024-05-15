using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.DTOs;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Incluir(UserDTO userDTO);
        Task<UserDTO> Alterar(UserDTO userDTO);
        Task<UserDTO> Excluir(int  idUser);
        Task<UserDTO> Get(int id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
