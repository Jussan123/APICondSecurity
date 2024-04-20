using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Domain.Models;
using APICondSecurity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
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
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.VeiculoTerceiro.FirstOrDefaultAsync(c => c.IdVeiculoTerceiro == IdVeiculoTerceiro);
#pragma warning restore CS8603 // Possível retorno de referência nula.
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
