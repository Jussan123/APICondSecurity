using APICondSecurity.DTOs;
using APICondSecurity.Infra.Data.Models;
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

        CreateMap<LayoutUnificadoCadastroUsuarioDTO, UserDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha))
            .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
            .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf));

        CreateMap<LayoutUnificadoCadastroUsuarioDTO, TipoUsuarioDTO> ()
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));

        CreateMap<LayoutUnificadoCadastroUsuarioDTO, ResidenciaDTO> ()
            .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
            .ForMember(dest => dest.Bloco, opt => opt.MapFrom(src => src.Bloco))
            .ForMember(dest => dest.Quadra, opt => opt.MapFrom(src => src.Quadra))
            .ForMember(dest => dest.Rua, opt => opt.MapFrom(src => src.Rua));

        CreateMap<LayoutUnificadoCadastroVeiculoDTO, VeiculoDTO> ().ReverseMap();
        CreateMap<LayoutUnificadoCadastroVeiculoDTO, VeiculoTerceiroDTO> ().ReverseMap();
        CreateMap<LayoutUnificadoCadastroVeiculoDTO, VeiculoUsuarioDTO> ().ReverseMap();
        CreateMap<LayoutUnificadoCadastroCondominoDTO, CondominioDTO> ().ReverseMap();
        CreateMap<LayoutUnificadoCadastroCondominoDTO, EnderecoDTO> ().ReverseMap();
        CreateMap<LayoutUnificadoCadastroCondominoDTO, CidadeDTO> ().ReverseMap();
        CreateMap<LayoutUnificadoCadastroCondominoDTO, UfDTO> ().ReverseMap();

    }
}
