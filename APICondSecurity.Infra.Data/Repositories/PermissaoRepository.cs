
using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class PermissaoRepository : IPermissao
    {
        private readonly condSecurityContext _context;

        public PermissaoRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Permissao permissao)
        {

            _context.Entry(permissao).State = EntityState.Modified;
        }

        public void Excluir(Permissao permissao)
        {
            try
            {
                _context.Permissao.Remove(permissao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Permissao permissao)
        {
            try
            {
                _context.Permissao.Add(permissao);
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

        public async Task<Permissao> Get(int IdPermissao)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.Permissao.FirstOrDefaultAsync(c => c.IdPermissao == IdPermissao);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar permissao: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Permissao>> GetAll()
        {
            try
            {
                return await _context.Permissao.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
