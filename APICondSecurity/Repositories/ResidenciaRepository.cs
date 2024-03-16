

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class ResidenciaRepository : IResidencia
    {
        private readonly condSecurityContext _context;

        public ResidenciaRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Residencia residencia)
        {

            _context.Entry(residencia).State = EntityState.Modified;
        }

        public void Excluir(Residencia residencia)
        {
            try
            {
                _context.Residencia.Remove(residencia);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Residencia residencia)
        {
            try
            {
                _context.Residencia.Add(residencia);
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

        public async Task<Residencia> Get(int IdResidencia)
        {
            try
            {
                return await _context.Residencia.FirstOrDefaultAsync(c => c.IdResidencia == IdResidencia);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar residencia: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Residencia>> GetAll()
        {
            try
            {
                return await _context.Residencia.ToListAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
