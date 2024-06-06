

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class VeiculoTerceiroRepository : IVeiculoTerceiro
    {
        private readonly condSecurityContext _context;

        public VeiculoTerceiroRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(VeiculoTerceiro veiculoTerceiro)
        {

            _context.Entry(veiculoTerceiro).State = EntityState.Modified;
        }

        public void Excluir(VeiculoTerceiro veiculoTerceiro)
        {
            try
            {
                _context.VeiculoTerceiro.Remove(veiculoTerceiro);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(VeiculoTerceiro veiculoTerceiro)
        {
            try
            {
                _context.VeiculoTerceiro.Add(veiculoTerceiro);
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

        public async Task<VeiculoTerceiro> Get(int IdVeiculoTerceiro)
        {
            try
            {
                return await _context.VeiculoTerceiro.FirstOrDefaultAsync(c => c.IdVeiculoTerceiro == IdVeiculoTerceiro);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar veiculoTerceiro: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<VeiculoTerceiro>> GetAll()
        {
            try
            {
                return await _context.VeiculoTerceiro.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
