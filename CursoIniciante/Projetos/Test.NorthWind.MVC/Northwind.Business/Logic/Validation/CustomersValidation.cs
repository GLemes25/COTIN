using FluentValidation;
using Northwind.Data.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Logic.Validation
{
    public class CustomersValidation : AbstractValidator<Customer>
    {
        public CustomersValidation()
        {
            RuleFor(c => c.CustomerID)
            .NotEmpty().WithMessage("O campo {Código do Cliente} precisa ser fornecido");

            RuleFor(c => c.CompanyName)
                .NotEmpty().WithMessage("O campo {Nome da Empresa} precisa ser fornecido");

            RuleFor(c => c.ContactName)
               .NotNull().WithMessage("O campo {Nome do Contato} precisa ser fornecido");

            RuleFor(c => c.ContactTitle)
                .NotNull().WithMessage("O campo {Título} precisa ser fornecido");

            RuleFor(c => c.Address)
                .NotNull().WithMessage("O campo {Endereço} precisa ser fornecido");

            RuleFor(c => c.City)
                .NotNull().WithMessage("O campo {Cidade} precisa ser fornecido");

            RuleFor(c => c.Region)
                .NotNull().WithMessage("O campo {Região} precisa ser fornecido");

            RuleFor(c => c.PostalCode)
                .NotNull().WithMessage("O campo {Código postal} precisa ser fornecido");

            RuleFor(c => c.Country)
                .NotNull().WithMessage("O campo {País} precisa ser fornecido");

            RuleFor(c => c.Phone)
                .NotNull().WithMessage("O campo {Telefone} precisa ser fornecido");

            RuleFor(c => c.Fax)
                .NotNull().WithMessage("O campo {Fax} precisa ser fornecido");
        }
    }
}

