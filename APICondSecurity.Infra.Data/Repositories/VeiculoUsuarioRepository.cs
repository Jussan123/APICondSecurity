﻿using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class VeiculoUsuarioRepository : IVeiculoUsuario
    {
        private readonly condSecurityContext _context;

        public VeiculoUsuarioRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(VeiculoUsuario veiculoUsuario)
        {

            _context.Entry(veiculoUsuario).State = EntityState.Modified;
        }

        public void Excluir(VeiculoUsuario veiculoUsuario)
        {
            try
            {
                _context.VeiculoUsuario.Remove(veiculoUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(VeiculoUsuario veiculoUsuario)
        {
            try
            {
                _context.VeiculoUsuario.Add(veiculoUsuario);
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

        public async Task<VeiculoUsuario> Get(int IdVeiculoUsuario)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.VeiculoUsuario.FirstOrDefaultAsync(c => c.IdVeiculoUsuario == IdVeiculoUsuario);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar veiculoUsuario: {ex.Message}");
                throw;
            }
        }

        public async Task<VeiculoUsuario> GetByRfid(int IdRfid)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.VeiculoUsuario.FirstOrDefaultAsync(c => c.IdRfid == IdRfid);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Erro ao buscar veiculo pelo RFID");
                throw;
            }
        }

        public async Task<IEnumerable<VeiculoUsuario>> GetAll()
        {
            try
            {
                return await _context.VeiculoUsuario.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}