using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.DTOs;
using AutoMapper;
using APICondSecurity.Infra.Data.Context;


namespace APICondSecurity.Infra.Data.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly IUsuario _repository;
        public readonly IMapper _mapper;
        private readonly condSecurityContext _context;

        public UsuarioService(IUsuario repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> Alterar(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioAlterado = await _repository.Alterar(usuario);
            return _mapper.Map<UsuarioDTO>(usuarioAlterado);
        }

        public async Task<UsuarioDTO> Excluir(int idUsuario)
        {
            var usuarioExcluido = await _repository.Excluir(idUsuario);
            return _mapper.Map<UsuarioDTO>(usuarioExcluido);
        }

        public async Task<UsuarioDTO> Get(int idUsuario)
        {
            var usuario = await _repository.Get(idUsuario);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAll()
        {
            var usuarios = await _repository.GetAll();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> Incluir(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioIncluido = await _repository.Incluir(usuario);
            return _mapper.Map<UsuarioDTO>(usuarioIncluido);
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                int linhasAfetadas = await _context.SaveChangesAsync();
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao Salvar as alterações no banco de dados: {ex.Message}");
                return false;
            }
        }
    }
}
