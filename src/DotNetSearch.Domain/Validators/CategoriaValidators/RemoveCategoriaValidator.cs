using DotNetSearch.Domain.Common;
using FluentValidation;
using System;

namespace DotNetSearch.Domain.Validators.CategoriaValidators
{
    public class RemoveCategoriaValidator : AbstractValidator<Guid>
    {
        public RemoveCategoriaValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);
        }
    }
}
