using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Models
{
    public class ReceptionistUpdateRequest
    {
        public string? ImageUrl { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public Guid OfficeId { get; set; }
    }
}
