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
            
            CreateMap<MotoRequest, Moto>();
            CreateMap<Moto, MotoResponse>()
                .ForMember(dest => dest.TagRfidId, opt => opt.MapFrom(src => src.TagRfid.Id))
                .ForMember(dest => dest.CodigoIdentificacao, opt => opt.MapFrom(src => src.TagRfid.CodigoIdentificacao));

            
            CreateMap<FilialRequest, Filial>();
            CreateMap<Filial, FilialResponse>();

            
            CreateMap<PatioRequest, Patio>();
            CreateMap<Patio, PatioResponse>();

            
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<Usuario, UsuarioResponse>();

            CreateMap<LeitorRfidRequest, LeitorRfid>();
            CreateMap<LeitorRfid, LeitorRfidResponse>();

            CreateMap<LeituraRfidRequest, LeituraRfid>();
            CreateMap<LeituraRfid, LeituraRfidResponse>();


        }
    }
}
