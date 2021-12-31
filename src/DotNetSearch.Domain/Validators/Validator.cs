using DotNetSearch.Domain.Entities;
using FluentValidation;

namespace DotNetSearch.Domain.Validators
{
    public abstract class Validator<T> : AbstractValidator<T> where T : Entity
    {
    }
}
