using DotNetSearch.Domain.Common;
using FluentValidation;
using System;

namespace DotNetSearch.Domain.Validators.AutorValidators
{
    public class RemoveAutorValidator : AbstractValidator<Guid>
    {
        public RemoveAutorValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);
        }
    }
}
