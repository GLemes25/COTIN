using FluentValidation;
using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.API.Logic.Validations
{
    public class SuppliersValidation : AbstractValidator<Suppliers>
    {
        public SuppliersValidation()
        {
            RuleFor(s => s.SupplierID)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(s => s.CompanyName)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(10, 150).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
