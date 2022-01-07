using DotNetSearch.Application.Contratos;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace DotNetSearch.Application.Interfaces
{
    public interface IAutorAppService : IAppService<AutorContrato>
    {
        Task<ValidationResult> Add(AutorContrato autorContrato);
        Task<ValidationResult> Update(AutorContrato autorContrato);
        Task<ValidationResult> Remove(Guid id);
    }
}
