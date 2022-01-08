using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using FluentValidation;

namespace DotNetSearch.Domain.Validators.AutorValidators
{
    public abstract class AutorValidator : Validator<Autor>
    {
        protected void ValidateRequired()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Nome").Message);

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("DataNascimento").Message);
        }
    }
}
