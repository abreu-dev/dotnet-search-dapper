using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Domain.Entities;

namespace DotNetSearch.Application.AutoMapper
{
    public class DotNetSearchMappingProfile : Profile
    {
        public DotNetSearchMappingProfile()
        {
            CreateMap<CategoriaContrato, Categoria>()
                .ReverseMap();
            CreateMap<Categoria, Categoria>();

            CreateMap<AutorContrato, Autor>()
                .ReverseMap();
            CreateMap<Autor, Autor>();

            CreateMap<LivroCategoria, CategoriaContrato>()
                .ForMember(categoriaContrato => categoriaContrato.Id,
                          member => member.MapFrom(source => source.CategoriaId))
                .ForMember(categoriaContrato => categoriaContrato.Nome,
                          member => member.MapFrom(source => source.Categoria.Nome))
                .ReverseMap()
                .ForMember(livroCategoria => livroCategoria.Id, expression => expression.Ignore())
                .ForMember(livroCategoria => livroCategoria.Livro, expression => expression.Ignore())
                .ForMember(livroCategoria => livroCategoria.LivroId, expression => expression.Ignore())
                .ForMember(livroCategoria => livroCategoria.Categoria, expression => expression.Ignore());

            CreateMap<LivroContrato, Livro>()
                .ForMember(x => x.Autor, expression => expression.Ignore())
                .ReverseMap();
            CreateMap<Livro, Livro>()
                .ForMember(x => x.Autor, expression => expression.Ignore());
        }
    }
}
