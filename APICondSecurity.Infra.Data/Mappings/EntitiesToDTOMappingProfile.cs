using APICondSecurity.Infra.Data.Models;
using APICondSecurity.DTOs;
using AutoMapper;

namespace APICondSecurity;

public class EntitiesToDTOMappingProfile : Profile
{
    public EntitiesToDTOMappingProfile()
    {
        CreateMap<Veiculo, VeiculoDTO>().ReverseMap();
        CreateMap<Cameras, CamerasDTO>().ReverseMap();
        CreateMap<Cidade, CidadeDTO>().ReverseMap();
        CreateMap<Condominio, CondominioDTO>().ReverseMap();
        CreateMap<Endereco, EnderecoDTO>().ReverseMap();
        CreateMap<Notificacao, NotificacaoDTO>().ReverseMap();
        CreateMap<Permissao, PermissaoDTO>().ReverseMap();
        CreateMap<Portao, PortaoDTO>().ReverseMap();
        CreateMap<Registros, RegistrosDTO>().ReverseMap();
        CreateMap<Residencia, ResidenciaDTO>().ReverseMap();
        CreateMap<Rfid, RfidDTO>().ReverseMap();
        CreateMap<TipoUsuario, TipoUsuarioDTO>().ReverseMap();
        CreateMap<Uf, UfDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        CreateMap<VeiculoTerceiro, VeiculoTerceiroDTO>().ReverseMap();
        CreateMap<VeiculoUsuario, VeiculoUsuarioDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();

    }
}
