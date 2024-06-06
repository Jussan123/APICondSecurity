using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LayoutUnificadoController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly ResidenciaRepository _residenciaRepository;
        private readonly TipoUsuarioRepository _tipoUsuarioRepository;
        private readonly VeiculoRepository _veiculoRepository;
        private readonly VeiculoTerceiroRepository _veiculoTerceiroRepository;
        private readonly VeiculoUsuarioRepository _veiculoUsuarioRepository;
        private readonly CondominioRepository _condominioRepository;
        private readonly EnderecoRepository _enderecoRepository;
        private readonly CidadeRepository _cidadeRepository;
        private readonly UfRepository _ufRepository;
        private readonly IMapper _mapper;

        public LayoutUnificadoController(UserRepository userRepository, ResidenciaRepository residenciaRepository, TipoUsuarioRepository tipoUsuarioRepository, VeiculoRepository veiculoRepository, VeiculoTerceiroRepository veiculoTerceiroRepository, VeiculoUsuarioRepository veiculoUsuarioRepository, CondominioRepository condominioRepository, EnderecoRepository enderecoRepository, CidadeRepository cidadeRepository, UfRepository ufRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _residenciaRepository = residenciaRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _veiculoRepository = veiculoRepository;
            _veiculoTerceiroRepository = veiculoTerceiroRepository;
            _veiculoUsuarioRepository = veiculoUsuarioRepository;
            _condominioRepository = condominioRepository;
            _enderecoRepository = enderecoRepository;
            _cidadeRepository = cidadeRepository;
            _ufRepository = ufRepository;
            _mapper = mapper;
        }

        [HttpPost("CadastroUnificadoDeUsuario")]
        public async Task<ActionResult> CadastroUnificadoDeUsuario(LayoutUnificadoCadastroUsuarioDTO layoutUnificadoCadastroUsuarioDTO)
        {
            var emailExiste = await _userRepository.UserExists(layoutUnificadoCadastroUsuarioDTO.Email);
            if (emailExiste)
            {
                return BadRequest("Este E-mail já possui um cadastro em nosso sistema.");
            }
                        
            var userDTO = (layoutUnificadoCadastroUsuarioDTO.Name,
                           layoutUnificadoCadastroUsuarioDTO.Email,
                           layoutUnificadoCadastroUsuarioDTO.Senha,
                           layoutUnificadoCadastroUsuarioDTO.Telefone,
                           layoutUnificadoCadastroUsuarioDTO.Situacao,
                           layoutUnificadoCadastroUsuarioDTO.Cpf);
            var user = _mapper.Map<User>(userDTO);
            user.SenhaSalt = [10];
            user.SenhaHash = Encoding.ASCII.GetBytes(layoutUnificadoCadastroUsuarioDTO.Senha);
            user.CpfSalt = [10];
            user.CpfHash = Encoding.ASCII.GetBytes(layoutUnificadoCadastroUsuarioDTO.Cpf);
            var tipoUsuarioDTO = (layoutUnificadoCadastroUsuarioDTO.Tipo);
            var tipoUsuario = _mapper.Map<TipoUsuario>(tipoUsuarioDTO);

            var residenciaDTO = (layoutUnificadoCadastroUsuarioDTO.Numero,
                                 layoutUnificadoCadastroUsuarioDTO.Bloco,
                                 layoutUnificadoCadastroUsuarioDTO.Quadra,
                                 layoutUnificadoCadastroUsuarioDTO.Rua);
            var residencia = _mapper.Map<Residencia>(residenciaDTO);

            _userRepository.Incluir(user);
            _tipoUsuarioRepository.Incluir(tipoUsuario);
            _residenciaRepository.Incluir(residencia);

            try
            {
                await _userRepository.SaveAllAsync();
                await _tipoUsuarioRepository.SaveAllAsync();
                await _residenciaRepository.SaveAllAsync();
                return Ok("Usuário cadastrado com Sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar o usuário: {ex.Message}");
            }
        }

        [HttpPost("CadastroUnificadoVeiculo")]
        public async Task<ActionResult> CadastroUnificadoVeiculo(LayoutUnificadoCadastroVeiculoDTO layoutUnificadoCadastroVeiculoDTO)
        {
            var veiculoDTO = (layoutUnificadoCadastroVeiculoDTO.Placa,
                              layoutUnificadoCadastroVeiculoDTO.Marca,
                              layoutUnificadoCadastroVeiculoDTO.Modelo,
                              layoutUnificadoCadastroVeiculoDTO.Cor,
                              layoutUnificadoCadastroVeiculoDTO.Ano,
                              layoutUnificadoCadastroVeiculoDTO.Situacao);
            var veiculo = _mapper.Map<Veiculo>(veiculoDTO);

            var veiculoTerceiroDTO = (layoutUnificadoCadastroVeiculoDTO.Placa,
                                      layoutUnificadoCadastroVeiculoDTO.IdUsuario);
            var veiculoTerceiro = _mapper.Map<VeiculoTerceiro>(veiculoTerceiroDTO);

            var veiculoUsuarioDTO = (layoutUnificadoCadastroVeiculoDTO.Placa,
                                      layoutUnificadoCadastroVeiculoDTO.IdUsuario,
                                      layoutUnificadoCadastroVeiculoDTO.IdRfid);
            var veiculoUsuario = _mapper.Map<VeiculoUsuario>(veiculoUsuarioDTO);

            _veiculoRepository.Incluir(veiculo);
            _veiculoTerceiroRepository.Incluir(veiculoTerceiro);
            _veiculoUsuarioRepository.Incluir(veiculoUsuario);

            try
            {
                await _veiculoRepository.SaveAllAsync();
                await _veiculoTerceiroRepository.SaveAllAsync();
                await _veiculoUsuarioRepository.SaveAllAsync();
                return Ok("Veículo cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar o veiculo: {ex.Message}");
            }
        }

        [HttpPost("CadastroUnificadoCondomino")]
        public async Task<ActionResult> CadastroUnificadoCondominio(LayoutUnificadoCadastroCondominoDTO layoutUnificadoCadastroCondominoDTO)
        {
            var condominioDTO = (layoutUnificadoCadastroCondominoDTO.Nome,
                                 layoutUnificadoCadastroCondominoDTO.Situacao);
            var condominio = _mapper.Map<Condominio>(condominioDTO);

            var enderecoDTO = (layoutUnificadoCadastroCondominoDTO.Rua,
                               layoutUnificadoCadastroCondominoDTO.Numero,
                               layoutUnificadoCadastroCondominoDTO.Bairro,
                               layoutUnificadoCadastroCondominoDTO.Cep,
                               layoutUnificadoCadastroCondominoDTO.Complemento);
            var endereco = _mapper.Map<Endereco>(enderecoDTO);

            var cidadeDTO = (layoutUnificadoCadastroCondominoDTO.NomeCidade,
                             layoutUnificadoCadastroCondominoDTO.CidadeIbge);
            var cidade = _mapper.Map<Cidade>(cidadeDTO);

            var ufDTO = (layoutUnificadoCadastroCondominoDTO.NomeUf,
                         layoutUnificadoCadastroCondominoDTO.Sigla);
            var uf = _mapper.Map<Uf>(ufDTO);
            _condominioRepository.Incluir(condominio);
            _enderecoRepository.Incluir(endereco);
            _cidadeRepository.Incluir(cidade);
            _ufRepository.Incluir(uf);

            try
            {
                await _condominioRepository.SaveAllAsync();
                await _enderecoRepository.SaveAllAsync();
                await _cidadeRepository.SaveAllAsync();
                await _ufRepository.SaveAllAsync();
                return Ok("Condominio cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar o condominio: {ex.Message}");
            }
        }
    }
}
