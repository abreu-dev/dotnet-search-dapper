using DotNetSearch.Application.Contratos;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace DotNetSearch.Application.Interfaces
{
    public interface ILivroAppService : IAppService<LivroContrato>
    {
        Task<ValidationResult> Add(LivroContrato livroContrato);
        Task<ValidationResult> Update(LivroContrato livroContrato);
        Task<ValidationResult> Remove(Guid id);
    }
}
