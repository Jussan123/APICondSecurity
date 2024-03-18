

using APICondSecurity.Interfaces;
using APICondSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Repositories
{
    public class NotificacaoRepository : INotificacao
    {
        private readonly condSecurityContext _context;

        public NotificacaoRepository(condSecurityContext context)
        {
            _context = context;
        }

        public void Alterar(Notificacao notificacao)
        {

            _context.Entry(notificacao).State = EntityState.Modified;
        }

        public void Excluir(Notificacao notificacao)
        {
            try
            {
                _context.Notificacao.Remove(notificacao);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public void Incluir(Notificacao notificacao)
        {
            try
            {
                _context.Notificacao.Add(notificacao);
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

        public async Task<Notificacao> Get(int IdNotificacao)
        {
            try
            {
                return await _context.Notificacao.FirstOrDefaultAsync(c => c.IdNotificacao == IdNotificacao);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar notificacao: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Notificacao>> GetAll()
        {
            try
            {
                return await _context.Notificacao.ToListAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
