using FluentValidation;
using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.API.Logic.Validations
{
    public class CategoriesValidation : AbstractValidator<Categories>
    {
        public CategoriesValidation()
        {
            RuleFor(c => c.CategoryName)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
           .Length(5, 15).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Description)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(10, 150).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
