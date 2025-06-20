using FluentValidation;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;

namespace ProductsManagement.Application.UseCases.ProductUseCase.Validations.InputValidations
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must be at most 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must be at most 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category is required.");
        }
    }
}
