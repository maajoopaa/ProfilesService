using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Models
{
    public class Receptionist
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImageUrl { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public Guid AccountId { get; set; }

        public Guid OfficeId { get; set; }
    }
}
