using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using FluentValidation;

namespace DotNetSearch.Domain.Validators.CategoriaValidators
{
    public abstract class CategoriaValidator : Validator<Categoria>
    {
        protected void ValidateRequired()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Nome").Message);
        }
    }
}
