using FluentValidation;
using NorthWind.Data.Logic.Data;

namespace Northwind.Business.Logic.Validation
{
    public class CategoriesValidation : AbstractValidator<Categories>
    {
        public CategoriesValidation()
        {
            RuleFor(c => c.CategoryName).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido").Length(2, 15).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Description).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido").Length(10, 150).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.CategoryID).NotEqual(0).WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}