using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Domain.Entitys;
using AutoMapper;

namespace AmazingBeer.Api.Infraestructure.Mappers
{
    public class CervejaProfile : Profile
    {
        public CervejaProfile()
        {
            // Entidade para Dto | Dto para Entidade - Cerveja
            CreateMap<CervejaEntity, ListarCervejaDto>().ReverseMap();
            CreateMap<CervejaEntity, CriarCervejaDto>().ReverseMap();
            CreateMap<CervejaEntity, EditarCervejaDto>().ReverseMap();
        }
    }
}
