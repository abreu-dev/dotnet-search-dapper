using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Domain.Entities;

namespace DotNetSearch.Application.AutoMapper
{
    public class DotNetSearchMappingProfile : Profile
    {
        public DotNetSearchMappingProfile()
        {
            CreateMap<Categoria, CategoriaContrato>()
                .ReverseMap();
            CreateMap<Categoria, Categoria>();
        }
    }
}
