

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class RegistrosRepository : IRegistros
    {
        private readonly condSecurityContext _context;

        public RegistrosRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Registros registros)
        {

            _context.Entry(registros).State = EntityState.Modified;
        }

        public void Excluir(Registros registros)
        {
            try
            {
                _context.Registros.Remove(registros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Registros registros)
        {
            try
            {
                _context.Registros.Add(registros);
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

        public async Task<Registros> Get(int IdRegistros)
        {
            try
            {
                return await _context.Registros.FirstOrDefaultAsync(c => c.IdRegistros == IdRegistros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar registros: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Registros>> GetAll()
        {
            try
            {
                return await _context.Registros.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
