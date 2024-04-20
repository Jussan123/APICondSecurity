using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Domain.Models;
using APICondSecurity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
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
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.Usuario.FirstOrDefaultAsync(c => c.IdUsuario == IdUsuario);
#pragma warning restore CS8603 // Possível retorno de referência nula.
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
