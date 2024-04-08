

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class VeiculoRepository : IVeiculo
    {
        private readonly condSecurityContext _context;

        public VeiculoRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Veiculo veiculo)
        {

            _context.Entry(veiculo).State = EntityState.Modified;
        }

        public void Excluir(Veiculo veiculo)
        {
            try
            {
                _context.Veiculo.Remove(veiculo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Veiculo veiculo)
        {
            try
            {
                _context.Veiculo.Add(veiculo);
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

        public async Task<Veiculo> Get(int IdVeiculo)
        {
            try
            {
                return await _context.Veiculo.FirstOrDefaultAsync(c => c.IdVeiculo == IdVeiculo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar veiculo: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Veiculo>> GetAll()
        {
            try
            {
                return await _context.Veiculo.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
