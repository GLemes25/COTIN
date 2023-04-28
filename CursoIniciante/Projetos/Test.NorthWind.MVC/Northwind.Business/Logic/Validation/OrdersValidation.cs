using FluentValidation;
using Northwind.Data.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Logic.Validation
{
    public class OrdersValidation : AbstractValidator<Order>
    {
        public OrdersValidation()
        {

            RuleFor(o => o.CustomerID).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(o => o.ShipName).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(o => o.ShipAddress).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(o => o.ShipCity).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(o => o.ShipRegion).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(o => o.ShipPostalCode).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(o => o.ShipCountry).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        }
    }
}
