using Domain;
using FluentValidation;

namespace Application.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(a => a.FirstName).NotEmpty().WithMessage("First Name is required.");
        RuleFor(a => a.LastName).NotEmpty().WithMessage("Last Name is required.");
        RuleFor(a => a.YearOfBirth).GreaterThan(0).WithMessage("Year of Birth must be a positive number.");
    }
}