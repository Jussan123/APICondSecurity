﻿using APICondSecurity.DTOs;
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
            _hubContext = hubContext;
            _mapper = mapper;
        }

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
    }
}