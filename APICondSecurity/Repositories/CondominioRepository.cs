﻿

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class CondominioRepository : ICondominio
    {
        private readonly condSecurityContext _context;

        public CondominioRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Condominio condominio)
        {

            _context.Entry(condominio).State = EntityState.Modified;
        }

        public void Excluir(Condominio condominio)
        {
            try
            {
                _context.Condominio.Remove(condominio);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Condominio condominio)
        {
            try
            {
                _context.Condominio.Add(condominio);
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

        public async Task<Condominio> Get(int IdCondominio)
        {
            try
            {
                return await _context.Condominio.FirstOrDefaultAsync(c => c.IdCondominio == IdCondominio);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar condominio: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Condominio>> GetAll()
        {
            try
            {
                return await _context.Condominio.ToListAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}