namespace Profiles.Domain.Models
{
    public class Doctor
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImageUrl { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid AccountId { get; set; }

        public Guid SpecializationId { get; set; }

        public Guid OfficeId { get; set; }

        public DateTime CareerStartYear { get; set; }

        public Status Status { get; set; }
    }
}
