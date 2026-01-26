using FluentValidation;

namespace Services;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        // Name validator
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Product name is required")
            .Length(3, 10).WithMessage("Product name must be between 3 and 10 characters");

        // Price validator
        RuleFor(request => request.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .NotNull().WithMessage("Price must be greater than 0");

        // Stock validator
        RuleFor(request => request.Stock)
            .InclusiveBetween(0, 100).WithMessage("Stock must be between 0 and 100");
    }
}