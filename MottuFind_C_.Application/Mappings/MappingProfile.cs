using AutoMapper;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;

namespace Sprint1_C_.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Moto
            CreateMap<MotoRequest, Moto>();
            CreateMap<Moto, MotoResponse>()
                .ForMember(dest => dest.TagRfidId, opt => opt.MapFrom(src => src.TagRfid.Id))
                .ForMember(dest => dest.CodigoIdentificacao, opt => opt.MapFrom(src => src.TagRfid.CodigoIdentificacao));

            // Filial
            CreateMap<FilialRequest, Filial>();
            CreateMap<Filial, FilialResponse>();

            // Patio
            CreateMap<PatioRequest, Patio>();
            CreateMap<Patio, PatioResponse>();

            //Usuario
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<Usuario, UsuarioResponse>();


        }
    }
}
