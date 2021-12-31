using DotNetSearch.Application.Contratos;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace DotNetSearch.Application.Interfaces
{
    public interface ICategoriaAppService : IAppService<CategoriaContrato> 
    {
        Task<ValidationResult> Add(CategoriaContrato categoriaContrato);
        Task<ValidationResult> Update(CategoriaContrato categoriaContrato);
        Task<ValidationResult> Remove(Guid id);
    }
}
