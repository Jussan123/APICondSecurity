﻿

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class CameraRepository : ICamera
    {
        private readonly condSecurityContext _context;

        public CameraRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Cameras camera)
        {

            _context.Entry(camera).State = EntityState.Modified;
        }

        public void Excluir(Cameras camera)
        {
            try
            {
                _context.Cameras.Remove(camera);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Cameras camera)
        {
            try
            {
                _context.Cameras.Add(camera);
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

        public async Task<Cameras> Get(int IdCamera)
        {
            try
            {
                return await _context.Cameras.FirstOrDefaultAsync(c => c.IdCamera == IdCamera);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar camera: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Cameras>> GetAll()
        {
            try
            {
                return await _context.Cameras.ToListAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}