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
using Npgsql.Internal;
using Azure.Core;

namespace APICondSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbrePortaoController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly UserRepository _userRepository;
        private readonly VeiculoRepository _veiculoRepository;
        private readonly VeiculoTerceiroRepository _veiculoTerceiroRepository;
        private readonly VeiculoUsuarioRepository _veiculoUsuarioRepository;
        private readonly RfidRepository _rfidRepository;
        private readonly RegistrosRepository _registrosRepository;
        private readonly PortaoRepository _portaoRepository;
        private readonly PermissaoRepository _permissaoRepository;
        private readonly ITemporaryStorageService _temporaryStorageService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IMapper _mapper;

        public AbrePortaoController(HttpClient httpClient,
                                         UserRepository userRepository,
                                         VeiculoRepository veiculoRepository,
                                         VeiculoTerceiroRepository veiculoTerceiroRepository,
                                         VeiculoUsuarioRepository veiculoUsuarioRepository,
                                         RfidRepository rfidRepository,
                                         RegistrosRepository registrosRepository,
                                         PortaoRepository portaoRepository,
                                         PermissaoRepository permissaoRepository,
                                         ITemporaryStorageService temporaryStorageService,
                                         IHubContext<NotificationHub> hubContext,
                                         IMapper mapper)
        {
            _httpClient = httpClient;
            _userRepository = userRepository;
            _veiculoRepository = veiculoRepository;
            _veiculoTerceiroRepository = veiculoTerceiroRepository;
            _veiculoUsuarioRepository = veiculoUsuarioRepository;
            _rfidRepository = rfidRepository;
            _registrosRepository = registrosRepository;
            _portaoRepository = portaoRepository;
            _permissaoRepository = permissaoRepository;
            _temporaryStorageService = temporaryStorageService;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        [HttpPost("ReceberRfid")]
        [Authorize]
        public ActionResult ReceberRfid([FromBody] RfidDTO rfidDTO)
        {
            try
            {
                _temporaryStorageService.StoreRfid(rfidDTO.Numero);
                return Ok("RFID recebido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao receber o RFID: {ex.Message}");
            }
        }

        [HttpPost("ReceberTerceiro")]
        [Authorize]
        public ActionResult ReceberTerceiro([FromBody] VeiculoTerceiroDTO veiculoTerceiroDTO)
        {
            try
            {
                _temporaryStorageService.StorePlaca(veiculoTerceiroDTO.Placa);
                return Ok("Terceiro recebido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao receber o Terceiro: {ex.Message}");
            }
        }

        [HttpPost("AberturaPortao")]
        [Authorize]
        public async Task<ActionResult> AbrePortao(LayoutUnificadoPlacaRfidDTO layoutUnificadoPlacaRfidDTO)
        {
            try
            {

                var rfid = _temporaryStorageService.GetRfid();
                if (rfid == null)
                {
                    return BadRequest("RFID não recebido.");
                }

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
                var rfidUse = _mapper.Map<Rfid>(rfidDTO);
                await _rfidRepository.GetByTag(rfidDTO.Numero);

                if (veiculoDTO.Situacao == "I")
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Veiculo Não autorizado placa:", layoutUnificadoPlacaRfidDTO.Placa);

                    return BadRequest("Veículo não autorizado.");
                }

                if (veiculo != null && rfidUse != null)
                {
                    try
                    {
                        // Verificação de RFID e outros dados necessários

                        // Configuração do URL do ESP32 (substitua pelo seu IP público e porta)
                        var esp32Url = "http://192.168.1.10:80/control";

                        // Comando a ser enviado ao ESP32 (no seu caso, um booleano)
                        // var command = new CommandModel
                        // {
                        //     Command = true
                        // };

                        var request = new HttpRequestMessage(HttpMethod.Post, esp32Url);
                        var content = new StringContent("{\"command\":true}", null, "application/json");
                        request.Content = content;

                        var response = await _httpClient.SendAsync(request);

                        // Tratamento da resposta do ESP32
                        if (response.IsSuccessStatusCode)
                        {
                            // Lógica adicional após o sucesso da requisição ao ESP32
                            return Ok("Comando enviado com sucesso para o ESP32");
                        }
                        else
                        {
                            // Tratamento de erro caso a requisição não tenha sido bem-sucedida
                            return StatusCode((int)response.StatusCode, "Erro ao enviar comando para o ESP32");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Captura de exceções que possam ocorrer durante o processamento da requisição
                        return BadRequest($"Ocorreu um erro ao enviar comando para o ESP32: {ex.Message}");
                    }

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
                            IdVeiculo = veiculo.IdVeiculo,
                            Tag = rfidUse.Numero
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
                            IdVeiculo = veiculo.IdVeiculo,
                            Tag = rfidUse.Numero
                        };
                        var registros = _mapper.Map<Registros>(registrosDTO);
                        _registrosRepository.Incluir(registros);
                        await _registrosRepository.SaveAllAsync();
                    }
                    
                    return Ok("Veículo Encontrado e Portão Aberto!");
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
                var placaTerceiro = _temporaryStorageService.GetPlaca();
                if (placaTerceiro == null)
                {
                    return BadRequest("Placa não encontrada!");
                }

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
                    var esp32Url = "http://192.168.248.169/control"; // Substitua pelo IP do ESP32
                    //var command = new { angle = 90 }; // Ajuste o ângulo conforme necessário
                    var command = true;
                    var response = await _httpClient.PostAsJsonAsync(esp32Url, command);

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
                            IdVeiculo = veiculo.IdVeiculo,
                            Tag = null
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
                            IdVeiculo = veiculo.IdVeiculo,
                            Tag = null
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
    }
}
