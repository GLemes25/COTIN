using FluentValidation;
using Northwind.Data.Logic.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Northwind.Business.Logic.Validation
{
    public class ProductsValidation : AbstractValidator<Products>
    {
        public ProductsValidation()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido").Length(3, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.CategoryID).NotEqual(0).WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.SupplierID).NotEqual(0).WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.QuantityPerUnit).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.UnitPrice).NotEqual(0).WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.UnitsInStock).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.UnitsOnOrder).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.ReorderLevel).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        }
    }
}
