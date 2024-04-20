
using APICondSecurity.Domain.Interfaces;
using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class EnderecoRepository : IEndereco
    {
        private readonly condSecurityContext _context;

        public EnderecoRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Endereco endereco)
        {

            _context.Entry(endereco).State = EntityState.Modified;
        }

        public void Excluir(Endereco endereco)
        {
            try
            {
                _context.Endereco.Remove(endereco);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Endereco endereco)
        {
            try
            {
                _context.Endereco.Add(endereco);
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

        public async Task<Endereco> Get(int IdEndereco)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.Endereco.FirstOrDefaultAsync(c => c.IdEndereco == IdEndereco);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar endereco: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Endereco>> GetAll()
        {
            try
            {
                return await _context.Endereco.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
