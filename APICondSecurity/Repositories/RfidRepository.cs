﻿

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
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
                return await _context.Rfid.FirstOrDefaultAsync(c => c.IdRfid == IdRfid);
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