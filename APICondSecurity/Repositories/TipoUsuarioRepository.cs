﻿

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        private readonly condSecurityContext _context;

        public TipoUsuarioRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(TipoUsuario tipoUsuario)
        {

            _context.Entry(tipoUsuario).State = EntityState.Modified;
        }

        public void Excluir(TipoUsuario tipoUsuario)
        {
            try
            {
                _context.TipoUsuario.Remove(tipoUsuario);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(TipoUsuario tipoUsuario)
        {
            try
            {
                _context.TipoUsuario.Add(tipoUsuario);
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

        public async Task<TipoUsuario> Get(int IdTipoUsuario)
        {
            try
            {
                return await _context.TipoUsuario.FirstOrDefaultAsync(c => c.IdTipoUsuario == IdTipoUsuario);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar tipoUsuario: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TipoUsuario>> GetAll()
        {
            try
            {
                return await _context.TipoUsuario.ToListAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}