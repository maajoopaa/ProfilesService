using FluentValidation;
using Profiles.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Validators
{
    public class PaginationModelValidator : AbstractValidator<PaginationModel>
    {
        public PaginationModelValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("page cannot be less then 1");
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("pagesize cannot be less then 1");
        }
    }
}
