using FluentValidation;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(_ => _.CategoryId).NotEmpty().GreaterThan(0).WithMessage("Categor can not be zero.");
            RuleFor(_ => _.Name).NotEmpty().WithMessage("Product Name cannot be empty.");
            RuleFor(_ => _.Price).NotEmpty().WithMessage("Price can not be empty.").GreaterThan(0).WithMessage("Price can not be greather then zero.");
            RuleFor(_ => _.StockQuantity).NotEmpty().WithMessage("Stock quantity cannot be empty.");
            RuleFor(_ => _.Details).NotEmpty().WithMessage("Product Details cannot be empty.");
        }
    }
}
