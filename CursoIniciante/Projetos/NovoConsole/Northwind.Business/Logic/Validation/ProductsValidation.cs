using FluentValidation;
using NorthWind.Data.Logic.Data;

namespace Northwind.Business.Logic.Validation
{
    public class ProductsValidation : AbstractValidator<Products>
    {
        public ProductsValidation()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 15).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


            RuleFor(p => p.CategoryID).NotEqual(0)
                .WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
