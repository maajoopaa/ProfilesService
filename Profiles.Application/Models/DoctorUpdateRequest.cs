using Profiles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Models
{
    public class DoctorUpdateRequest
    {
        public string? ImageUrl { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid SpecializationId { get; set; }

        public Guid OfficeId { get; set; }

        public DateTime CareerStartYear { get; set; }

        public Status Status { get; set; }
    }
}
