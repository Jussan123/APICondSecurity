using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using APICondSecurity.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LayoutUnificadoController : ControllerBase
    {
        //private readonly HttpClient _httpClient;
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
        private readonly RfidRepository _rfidRepository;
        //private readonly RegistrosRepository _registrosRepository;
        //private readonly PortaoRepository _portaoRepository;
        //private readonly PermissaoRepository _permissaoRepository;
        //private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IMapper _mapper;

        public LayoutUnificadoController(//HttpClient httpClient,
                                         UserRepository userRepository,
                                         ResidenciaRepository residenciaRepository,
                                         TipoUsuarioRepository tipoUsuarioRepository,
                                         VeiculoRepository veiculoRepository,
                                         VeiculoTerceiroRepository veiculoTerceiroRepository,
                                         VeiculoUsuarioRepository veiculoUsuarioRepository,
                                         CondominioRepository condominioRepository,
                                         EnderecoRepository enderecoRepository,
                                         CidadeRepository cidadeRepository,
                                         UfRepository ufRepository,
                                         RfidRepository rfidRepository,
                                         //RegistrosRepository registrosRepository,
                                         //PortaoRepository portaoRepository,
                                         //PermissaoRepository permissaoRepository,
                                         //IHubContext<NotificationHub> hubContext,
                                         IMapper mapper)
        {
            //_httpClient = httpClient;
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
            _rfidRepository = rfidRepository;
            //_registrosRepository = registrosRepository;
            //_portaoRepository = portaoRepository;
            //_permissaoRepository = permissaoRepository;
            //_hubContext = hubContext;
            _mapper = mapper;
        }

        [HttpPost("CadastroUnificadoDeUsuario")]
        [Authorize]
        public async Task<ActionResult> CadastroUnificadoDeUsuario(LayoutUnificadoCadastroUsuarioDTO layoutUnificadoCadastroUsuarioDTO)
        {
            var emailExiste = await _userRepository.UserExists(layoutUnificadoCadastroUsuarioDTO.Email);
            if (emailExiste)
            {
                return BadRequest("Este E-mail já possui um cadastro em nosso sistema.");
            }

            try
            {
                UserDTO userDTO = new UserDTO()
                {
                    Name = layoutUnificadoCadastroUsuarioDTO.Name,
                    Email = layoutUnificadoCadastroUsuarioDTO.Email,
                    Senha = layoutUnificadoCadastroUsuarioDTO.Senha,
                    Telefone = layoutUnificadoCadastroUsuarioDTO.Telefone,
                    Situacao = layoutUnificadoCadastroUsuarioDTO.Situacao,
                    Cpf = layoutUnificadoCadastroUsuarioDTO.Cpf,
                    IdTipoUsuario = layoutUnificadoCadastroUsuarioDTO.IdTipoUsuario
                };

                var user = _mapper.Map<User>(userDTO);

                using var hmac = new HMACSHA512();
                byte[] SenhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Senha));
                byte[] SenhaSalt = hmac.Key;
                byte[] CpfHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Cpf));
                byte[] CpfSalt = hmac.Key;

                user.AlterarSenha(SenhaHash, SenhaSalt);
                user.CpfHash = CpfHash;
                user.CpfSalt = CpfSalt;

                ResidenciaDTO residenciaDTO = new ResidenciaDTO()
                {
                    Numero = layoutUnificadoCadastroUsuarioDTO.Numero,
                    Bloco = layoutUnificadoCadastroUsuarioDTO.Bloco,
                    Quadra = layoutUnificadoCadastroUsuarioDTO.Quadra,
                    Rua = layoutUnificadoCadastroUsuarioDTO.Rua,
                    IdCondominio = layoutUnificadoCadastroUsuarioDTO.IdCondominio
                };

                var residencia = _mapper.Map<Residencia>(residenciaDTO);

                _userRepository.Incluir(user);
                _residenciaRepository.Incluir(residencia);

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
        [Authorize]
        public async Task<ActionResult> CadastroUnificadoVeiculo(LayoutUnificadoCadastroVeiculoDTO layoutUnificadoCadastroVeiculoDTO)
        {
            try
            {
                VeiculoDTO veiculoDTO = new VeiculoDTO()
                {
                    Placa = layoutUnificadoCadastroVeiculoDTO.Placa,
                    Marca = layoutUnificadoCadastroVeiculoDTO.Marca,
                    Modelo = layoutUnificadoCadastroVeiculoDTO.Modelo,
                    Cor = layoutUnificadoCadastroVeiculoDTO.Cor,
                    Ano = layoutUnificadoCadastroVeiculoDTO.Ano,
                    Situacao = layoutUnificadoCadastroVeiculoDTO.Situacao,
                    IdUsuario = layoutUnificadoCadastroVeiculoDTO.IdUsuario
                };
                var veiculo = _mapper.Map<Veiculo>(veiculoDTO);


                VeiculoUsuarioDTO veiculoUsuarioDTO = new VeiculoUsuarioDTO()
                {
                    Placa = layoutUnificadoCadastroVeiculoDTO.Placa,
                    IdUsuario = layoutUnificadoCadastroVeiculoDTO.IdUsuario,
                    IdRfid = layoutUnificadoCadastroVeiculoDTO.IdRfid
                };
                var veiculoUsuario = _mapper.Map<VeiculoUsuario>(veiculoUsuarioDTO);

                _veiculoRepository.Incluir(veiculo);
                if (veiculoUsuario != null)
                {
                    _veiculoUsuarioRepository.Incluir(veiculoUsuario);
                    await _veiculoUsuarioRepository.SaveAllAsync();
                    string PlacaTerceiro = null;
                } else
                {
                    string PlacaTerceiro = layoutUnificadoCadastroVeiculoDTO.Placa;


                VeiculoTerceiroDTO veiculoTerceiroDTO = new VeiculoTerceiroDTO()
                {
                    Placa = PlacaTerceiro,
                    IdUsuario = layoutUnificadoCadastroVeiculoDTO.IdUsuario
                };
                var veiculoTerceiro = _mapper.Map<VeiculoTerceiro>(veiculoTerceiroDTO);

                if (veiculoTerceiro != null)
                {
                    _veiculoTerceiroRepository.Incluir(veiculoTerceiro);
                    await _veiculoTerceiroRepository.SaveAllAsync();
                }
                }

                await _veiculoRepository.SaveAllAsync();
                return Ok("Veículo cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao salvar o veiculo: {ex.Message}");
            }
        }

        [HttpPost("CadastroUnificadoCondomino")]
        [Authorize]
        public async Task<ActionResult> CadastroUnificadoCondominio(LayoutUnificadoCadastroCondominoDTO layoutUnificadoCadastroCondominoDTO)
        {
            try
            {
                

                EnderecoDTO enderecoDTO = new EnderecoDTO()
                {
                    Rua = layoutUnificadoCadastroCondominoDTO.Rua,
                    Numero = layoutUnificadoCadastroCondominoDTO.Numero,
                    Bairro = layoutUnificadoCadastroCondominoDTO.Bairro,
                    Cep = layoutUnificadoCadastroCondominoDTO.Cep,
                    Complemento = layoutUnificadoCadastroCondominoDTO.Complemento
                };
                var endereco = _mapper.Map<Endereco>(enderecoDTO);
                _enderecoRepository.Incluir(endereco);
                

                var enderecoId = await _enderecoRepository.Get(endereco.IdEndereco);

                CondominioDTO condominioDTO = new CondominioDTO()
                {
                    Nome = layoutUnificadoCadastroCondominoDTO.Nome,
                    Situacao = layoutUnificadoCadastroCondominoDTO.Situacao,
                    IdEndereco = enderecoId.IdEndereco
                };
                var condominio = _mapper.Map<Condominio>(condominioDTO);

                CidadeDTO cidadeDTO = new CidadeDTO()
                {
                    Nome = layoutUnificadoCadastroCondominoDTO.NomeCidade,
                    CidadeIbge = layoutUnificadoCadastroCondominoDTO.CidadeIbge
                };
                var cidade = _mapper.Map<Cidade>(cidadeDTO);

                UfDTO ufDTO = new UfDTO()
                {
                    Nome = layoutUnificadoCadastroCondominoDTO.NomeUf,
                    Sigla = layoutUnificadoCadastroCondominoDTO.Sigla
                };
                var uf = _mapper.Map<Uf>(ufDTO);

                _condominioRepository.Incluir(condominio);
                
                _cidadeRepository.Incluir(cidade);
                _ufRepository.Incluir(uf);

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
        /*
        [HttpPost("AberturaPortao")]
        [Authorize]
        public async Task<ActionResult> AbrePortao(LayoutUnificadoPlacaRfidDTO layoutUnificadoPlacaRfidDTO)
        {
            try
            {
                VeiculoDTO veiculoDTO = new VeiculoDTO()
                {
                    Placa = layoutUnificadoPlacaRfidDTO.Placa
                };
                var veiculo = _mapper.Map<Veiculo>(veiculoDTO);
                await _veiculoRepository.GetByPlaca(veiculoDTO.Placa);

                var veiculoUser = await _veiculoUsuarioRepository.GetByPlaca(veiculoDTO.Placa);

                RfidDTO rfidDTO = new RfidDTO()
                {
                    Numero = layoutUnificadoPlacaRfidDTO.Numero
                };
                var rfid = _mapper.Map<Rfid>(rfidDTO);
                await _rfidRepository.GetByTag(rfidDTO.Numero);

                if (veiculoDTO.Situacao == "I")
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Veiculo Não autorizado placa:", layoutUnificadoPlacaRfidDTO.Placa);

                    return BadRequest("Veículo não autorizado.");
                }

                if (veiculo != null && rfid != null)
                {
                    var esp32Url = "http://localhost/control"; // Substitua pelo IP do ESP32
                    //var command = new { angle = 90 }; // Ajuste o ângulo conforme necessário
                    var command = true;
                    //var response = await _httpClient.PostAsJsonAsync(esp32Url, command);

                    var portao = await _portaoRepository.Get(layoutUnificadoPlacaRfidDTO.IdPortao);

                    if (portao.Nome == "Entrada")
                    {
                        RegistrosDTO registrosDTO = new RegistrosDTO()
                        {
                            DataHoraEntrada = DateTime.Now,
                            DataHoraSaida = null,
                            Placa = layoutUnificadoPlacaRfidDTO.Placa,
                            IdVeiculoUsuario = veiculoUser.IdVeiculoUsuario,
                            IdPortao = portao.IdPortao,
                            IdVeiculoTerceiro = null,
                            IdUsuario = veiculoUser.IdUsuario,
                            IdVeiculo = veiculo.IdVeiculo
                        };
                        var registros = _mapper.Map<Registros>(registrosDTO);
                        _registrosRepository.Incluir(registros);
                        await _registrosRepository.SaveAllAsync();
                    }
                    else
                    {
                        RegistrosDTO registrosDTO = new RegistrosDTO()
                        {
                            DataHoraEntrada = null,
                            DataHoraSaida = DateTime.Now,
                            Placa = layoutUnificadoPlacaRfidDTO.Placa,
                            IdVeiculoUsuario = veiculoUser.IdVeiculoUsuario,
                            IdPortao = portao.IdPortao,
                            IdVeiculoTerceiro = null,
                            IdUsuario = veiculoUser.IdUsuario,
                            IdVeiculo = veiculo.IdVeiculo
                        };
                        var registros = _mapper.Map<Registros>(registrosDTO);
                        _registrosRepository.Incluir(registros);
                        await _registrosRepository.SaveAllAsync();
                    }

                    //if (response.IsSuccessStatusCode)
                    if (command == true)
                    {
                        return Ok("Veículo Encontrado e Portão Aberto!");
                    }
                    else
                    {
                        return BadRequest("entrou no if do command");// StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
                }

                return BadRequest("Veículo ou RFID não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar Veiculo e Rfid.{ex.Message}");
            }
        }

        [HttpPost("AberturaPortaoTerceiro")]
        [Authorize]
        public async Task<ActionResult> AbrePortaoterceiro(LayoutUnificadoPlacaTerceiroDTO layoutUnificadoPlacaTerceiroDTO)
        {
            try
            {
                VeiculoDTO veiculoDTO = new VeiculoDTO()
                {
                    Placa = layoutUnificadoPlacaTerceiroDTO.Placa
                };
                var veiculo = _mapper.Map<Veiculo>(veiculoDTO);
                await _veiculoRepository.GetByPlaca(veiculoDTO.Placa);

                PermissaoDTO permissaoDTO = new PermissaoDTO()
                {
                    IdPermissao = layoutUnificadoPlacaTerceiroDTO.IdPermissao
                };
                var permissao = _mapper.Map<Permissao>(permissaoDTO);
                await _permissaoRepository.Get(permissaoDTO.IdPermissao);

                if (permissao.Situacao == 'N')
                {
                    return BadRequest("Permissão não autorizada.");
                }

                var veiculoTerceiro = await _veiculoTerceiroRepository.GetByPlaca(veiculoDTO.Placa);
                
                if(veiculoDTO.Situacao == "I")
                {
                    return BadRequest("Veículo não autorizado.");
                }
                

                if (veiculo != null)
                {
                    var esp32Url = "http://localhost/control"; // Substitua pelo IP do ESP32
                    //var command = new { angle = 90 }; // Ajuste o ângulo conforme necessário
                    var command = true;
                    //var response = await _httpClient.PostAsJsonAsync(esp32Url, command);

                    var portao = await _portaoRepository.Get(layoutUnificadoPlacaTerceiroDTO.IdPortao);

                    if (portao.Nome == "Entrada")
                    {
                        RegistrosDTO registrosDTO = new RegistrosDTO()
                        {
                            DataHoraEntrada = DateTime.Now,
                            DataHoraSaida = null,
                            Placa = layoutUnificadoPlacaTerceiroDTO.Placa,
                            IdVeiculoUsuario = null,
                            IdPortao = portao.IdPortao,
                            IdVeiculoTerceiro = veiculoTerceiro.IdVeiculoTerceiro,
                            IdUsuario = veiculoTerceiro.IdUsuario,
                            IdVeiculo = veiculo.IdVeiculo
                        };
                        var registros = _mapper.Map<Registros>(registrosDTO);
                        _registrosRepository.Incluir(registros);
                        await _registrosRepository.SaveAllAsync();
                    }
                    else
                    {
                        RegistrosDTO registrosDTO = new RegistrosDTO()
                        {
                            DataHoraEntrada = null,
                            DataHoraSaida = DateTime.Now,
                            Placa = layoutUnificadoPlacaTerceiroDTO.Placa,
                            IdVeiculoUsuario = null,
                            IdPortao = portao.IdPortao,
                            IdVeiculoTerceiro = veiculoTerceiro.IdVeiculoTerceiro,
                            IdUsuario = veiculoTerceiro.IdUsuario,
                            IdVeiculo = veiculo.IdVeiculo
                        };
                        var registros = _mapper.Map<Registros>(registrosDTO);
                        _registrosRepository.Incluir(registros);
                        await _registrosRepository.SaveAllAsync();
                    }

                    //if (response.IsSuccessStatusCode)
                    if (command == true)
                    {
                        return Ok("Veículo Encontrado e Portão Aberto!");
                    }
                    else
                    {
                        return BadRequest("entrou no if do command");//StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
                }
                return BadRequest("Veículo não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Não foi liberado o acesso ao terceiro. {ex.Message}");
            }
        }
        */
    }
}
