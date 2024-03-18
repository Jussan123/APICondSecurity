

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly condSecurityContext _context;

        public UsuarioRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Usuario usuario)
        {

            _context.Entry(usuario).State = EntityState.Modified;
        }

        public void Excluir(Usuario usuario)
        {
            try
            {
                _context.Usuario.Remove(usuario);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Usuario usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
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
    }
}
