using Domain;
using FluentValidation;

namespace Application.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(b => b.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(b => b.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(b => b.Rating).InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");
        RuleFor(b => b.PublicationDate).NotEmpty().WithMessage("Publication Date is required.");
    }
}