﻿using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class PortaoRepository : IPortao
    {
        private readonly condSecurityContext _context;

        public PortaoRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Portao portao)
        {

            _context.Entry(portao).State = EntityState.Modified;
        }

        public void Excluir(Portao portao)
        {
            try
            {
                _context.Portao.Remove(portao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Portao portao)
        {
            try
            {
                _context.Portao.Add(portao);
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

        public async Task<Portao> Get(int IdPortao)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.Portao.FirstOrDefaultAsync(c => c.IdPortao == IdPortao);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar portao: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Portao>> GetAll()
        {
            try
            {
                return await _context.Portao.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}