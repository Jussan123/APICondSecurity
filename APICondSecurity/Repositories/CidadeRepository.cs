

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class CidadeRepository : ICidade
    {
        private readonly condSecurityContext _context;

        public CidadeRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Cidade cidade)
        {

            _context.Entry(cidade).State = EntityState.Modified;
        }

        public void Excluir(Cidade cidade)
        {
            try
            {
                _context.Cidade.Remove(cidade);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Cidade cidade)
        {
            try
            {
                _context.Cidade.Add(cidade);
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

        public async Task<Cidade> Get(int IdCidade)
        {
            try
            {
                return await _context.Cidade.FirstOrDefaultAsync(c => c.IdCidade == IdCidade);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar cidade: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Cidade>> GetAll()
        {
            try
            {
                return await _context.Cidade.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
