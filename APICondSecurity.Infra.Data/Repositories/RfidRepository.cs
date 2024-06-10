using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class RfidRepository : IRfid
    {
        private readonly condSecurityContext _context;

        public RfidRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Rfid rfid)
        {

            _context.Entry(rfid).State = EntityState.Modified;
        }

        public void Excluir(Rfid rfid)
        {
            try
            {
                _context.Rfid.Remove(rfid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Rfid rfid)
        {
            try
            {
                _context.Rfid.Add(rfid);
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

        public async Task<Rfid> Get(int IdRfid)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.Rfid.FirstOrDefaultAsync(c => c.IdRfid == IdRfid);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar rfid: {ex.Message}");
                throw;
            }
        }

        public async Task<Rfid> GetByTag(string numero)
        {
            try
            {
                return await _context.Rfid.FirstOrDefaultAsync(c => c.Numero == numero);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar rfid: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Rfid>> GetAll()
        {
            try
            {
                return await _context.Rfid.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
