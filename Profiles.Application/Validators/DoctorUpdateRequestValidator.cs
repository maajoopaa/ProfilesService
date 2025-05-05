using FluentValidation;
using Profiles.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Validators
{
    public class DoctorUpdateRequestValidator : AbstractValidator<DoctorUpdateRequest>
    {
        public DoctorUpdateRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(40).WithMessage("firstname cannot be empty or more then 40");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(40).WithMessage("lastname cannot be empty or more then 40");
            RuleFor(x => x.MiddleName).MaximumLength(40).WithMessage("middlename cannot be more then 40");
        }
    }
}
