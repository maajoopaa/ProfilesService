using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Models
{
    public class Specialization
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
