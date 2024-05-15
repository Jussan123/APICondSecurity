using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class UserRepository : IUser
    {
        private readonly condSecurityContext _context;
        public readonly IUser _repository;
        public readonly IMapper _mapper;

        public UserRepository(IUser repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UserRepository(condSecurityContext context)
        {
            _context = context;
        }

        public async Task<User> Alterar(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                return _mapper.Map<User>(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }

        }

        public async Task<User> Excluir(int idUser)
        {
            try
            {
                var userExcluido = await _repository.Excluir(idUser);
                return _mapper.Map<User>(userExcluido);
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
                return _mapper.Map<User>(user);
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
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.User.FirstOrDefaultAsync(c => c.id_user == IdUser);
#pragma warning restore CS8603 // Possível retorno de referência nula.
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

        public async Task<User> ExcluirUser(User user)
        {
            try
            {
                var userExcluido = await _repository.ExcluirUser(user);
                return _mapper.Map<User>(userExcluido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }
    }
}
