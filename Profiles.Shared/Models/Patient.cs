namespace Profiles.Domain.Models
{
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImageUrl { get; set; }

        public string FistName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid? AccountId { get; set; }
    }
}
