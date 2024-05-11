using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly condSecurityContext _context;
        public readonly IUsuario _repository;
        public readonly IMapper _mapper;

        public UsuarioRepository(IUsuario repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UsuarioRepository(condSecurityContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Alterar(Usuario usuario)
        {
            try
            {
                _context.Entry(usuario).State = EntityState.Modified;
                return _mapper.Map<Usuario>(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }

        }

        public async Task<Usuario> Excluir(int idUsuario)
        {
            try
            {
                var usuarioExcluido = await _repository.Excluir(idUsuario);
                return _mapper.Map<Usuario>(usuarioExcluido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task<Usuario> Incluir(Usuario usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return _mapper.Map<Usuario>(usuario);
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

        public async Task<Usuario> Get(int IdUsuario)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await _context.Usuario.FirstOrDefaultAsync(c => c.IdUsuario == IdUsuario);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar usuario: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            try
            {
                return await _context.Usuario.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Usuario> ExcluirUser(Usuario usuario)
        {
            try
            {
                var usuarioExcluido = await _repository.ExcluirUser(usuario);
                return _mapper.Map<Usuario>(usuarioExcluido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }
    }
}
