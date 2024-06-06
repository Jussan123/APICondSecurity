using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.DTOs;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;


namespace APICondSecurity.Infra.Data.Services
{
    public class UserService : IUserService
    {
        private readonly condSecurityContext _context;
        public readonly IUser _repository;
        public readonly IMapper _mapper;

       //public UserService(IUser repository, IMapper mapper)
       //{
       //    _repository = repository;
       //    _mapper = mapper;
       //}

         public UserService(condSecurityContext context, IMapper mapper, IUser repository)
         {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }
        
        public async Task<UserDTO> Alterar(UserDTO userDTO)
        {
            try
            {

                _context.Entry(userDTO).State = EntityState.Modified;
                return _mapper.Map<UserDTO>(userDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }

        }

        public async Task<UserDTO> Excluir(int idUserDTO)
        {
            try
            {
                var userExcluido = await _repository.Excluir(idUserDTO);
                return _mapper.Map<UserDTO>(userExcluido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task<UserDTO> Incluir(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var userIncluido = await _repository.Incluir(user);
                _context.SaveChanges();
                return _mapper.Map<UserDTO>(userIncluido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                int linhasAfetadas = await _context.SaveChangesAsync();
                return linhasAfetadas > 0;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Erro ao salvar as alterações no banco de dados: {ex.Message}");
                return false;
            }
        }

        public async Task<UserDTO> Get(int IdUser)
        {
            try
            {
                var user = await _repository.Get(IdUser);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar userDTO: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            try
            {
                var users = await _repository.GetAll();
                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<UserDTO> ExcluirUserDTO(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var userDTOExcluido = await _repository.ExcluirUser(user);
                return _mapper.Map<UserDTO>(userDTOExcluido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task<UserDTO> Login(string email, string senha)
       {
            try
            {
                var user = await _repository.Login(email, senha);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
