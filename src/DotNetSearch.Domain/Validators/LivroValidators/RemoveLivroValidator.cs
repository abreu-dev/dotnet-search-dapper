using DotNetSearch.Domain.Common;
using FluentValidation;
using System;

namespace DotNetSearch.Domain.Validators.LivroValidators
{
    public class RemoveLivroValidator : AbstractValidator<Guid>
    {
        public RemoveLivroValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);
        }
    }
}
