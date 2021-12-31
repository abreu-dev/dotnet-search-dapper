using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using FluentValidation;

namespace DotNetSearch.Domain.Validators
{
    public abstract class Validator<T> : AbstractValidator<T> where T : Entity
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);
        }
    }
}
