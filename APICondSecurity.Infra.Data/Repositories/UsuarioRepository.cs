using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly condSecurityContext _context;
        public readonly IUsuario _repository; // SEMPRE NULO
        public readonly IMapper _mapper; // SEMPRE NULO

        public UsuarioRepository(condSecurityContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Alterar(Usuario usuario)
        {
            try
            {
                _context.Entry(usuario).State = EntityState.Modified;
                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }

        }

        public async Task<bool> Excluir(int idUsuario)
        {
            try
            {
                var usuarioExcluido = await _repository.Excluir(idUsuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task<Usuario> Incluir(Usuario usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException( ex.Source+ ": " + ex.Message + "\n " + ex.InnerException);
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

        public async Task<Usuario> Get(int IdUsuario)
        {
            try
            {
                return await _context.Usuario.FirstOrDefaultAsync(c => c.IdUsuario == IdUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar usuario: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            try
            {
                return await _context.Usuario.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> ExcluirUser(Usuario usuario)
        {
            try
            {
                var usuarioExcluido = await _repository.ExcluirUser(usuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }
    }
}
