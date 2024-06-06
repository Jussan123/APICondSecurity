using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Services
{
    public class UserService : IUserService
    {
        private readonly condSecurityContext _context;
        public readonly IUser _repository;

         public UserService(condSecurityContext context)
         {
            _context = context;
         }

        public UserService(IUser repository)
        {
            _repository = repository;
        }

        public async Task<User> Alterar(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }

        }

        public async Task<bool> Excluir(int idUserDTO)
        {
            try
            {
                var userExcluido = await _repository.Excluir(idUserDTO);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task<User> Incluir(User user)
        {
            try
            {

                _context.User.Add(user);
                _context.SaveChanges();
                return user;
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

        public async Task<User> Get(int IdUser)
        {
            try
            {
                return await _context.User.FirstOrDefaultAsync(c => c.Id_user == IdUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar user: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.User.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> ExcluirUser(User user)
        {
            try
            {
                var userExcluido = await _repository.ExcluirUser(user);
                return userExcluido;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task<User> Login(string email, string senha)
       {
            try
            {
                var user = await _repository.Login(email, senha);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /*public async Task<User> LoginAplicacoes(string email, string senha, string token)
        {
            try
            {
                var user = await IUserService.LoginAplicacoes(email, senha, token);
                return _mapper.Map<User>(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }*/
    }
}
