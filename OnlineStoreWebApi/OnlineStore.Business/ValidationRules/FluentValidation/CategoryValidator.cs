using FluentValidation;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.ValidationRules.FluentValidation
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().WithMessage("Category Name can not be empty.");
            RuleFor(_ => _.Description).NotEmpty().WithMessage("Description cannot be empty.");
        }
    }
}
