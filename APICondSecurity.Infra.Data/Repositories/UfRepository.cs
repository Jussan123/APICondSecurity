﻿using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class UfRepository : IUf
    {
        private readonly condSecurityContext _context;

        public UfRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Uf uf)
        {

            _context.Entry(uf).State = EntityState.Modified;
        }

        public void Excluir(Uf uf)
        {
            try
            {
                _context.Uf.Remove(uf);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Uf uf)
        {
            try
            {
                _context.Uf.Add(uf);
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

        public async Task<Uf> Get(int IdUf)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.Uf.FirstOrDefaultAsync(c => c.IdUf == IdUf);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar uf: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Uf>> GetAll()
        {
            try
            {
                return await _context.Uf.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
