using FluentValidation;
using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.API.Logic.Validations
{
    public class ProdutosValidations : AbstractValidator<Products>
    {
        public ProdutosValidations()
        {
            RuleFor(p => p.ProductName)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
           .Length(5, 15).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.QuantityPerUnit)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(10, 150).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }

}
