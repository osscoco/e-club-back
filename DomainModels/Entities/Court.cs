using System.Text.Json.Serialization;

namespace DomainModels.Entities
{
    public class Court
    {
        public Guid CourtId { get; set; }
        public required string Name { get; set; }
        public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        public Guid? ClubId { get; set; }
        [JsonIgnore]
        public Club? Club { get; private set; } = null!;
        [JsonIgnore]
        public ICollection<Reservation>? Reservations { get; private set; } = new List<Reservation>();
        [JsonIgnore]
        public ICollection<Training>? Trainings { get; private set; } = new List<Training>();

        public Court() { }
    }
}
