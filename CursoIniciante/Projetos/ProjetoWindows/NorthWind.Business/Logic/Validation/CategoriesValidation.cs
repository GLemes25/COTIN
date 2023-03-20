using FluentValidation;
using FluentValidation.Validators;
using NorthWind.Data.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Business.Logic.Validation
{
    public class CategoriesValidation : AbstractValidator<Categories>
    {
        public CategoriesValidation()
        {
             RuleFor(c => c.CategoryName)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 15).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Description)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(10, 150).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.CategoryID)
            .NotEqual(0).WithMessage("O campo {PropertyName} precisa ser fornecido");
        }

    }
}