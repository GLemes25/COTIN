using FluentValidation;
using NorthWind.Data.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Business.Logic.Validation
{
    public class ProductsValidation : AbstractValidator<Products>
    {
        public ProductsValidation()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty().WithMessage("O campo {ProductName} precisa ser fornecido")
                .Length(0, 40).WithMessage("O campo {ProductName} precisa ter entre {MinLength} e {MaxLength} caracteres");
           
            RuleFor(c => c.ProductName)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(0, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Discontinued)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

           

        }
    }
}
