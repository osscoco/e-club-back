using DomainModels.Enums;
using System.Text.Json.Serialization;

namespace DomainModels.Entities
{
    public class Training
    {
        public Guid TrainingId { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public ReservationStatus? Status { get; set; } = ReservationStatus.PENDING;
        public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        public Guid? CourtId { get; set; }
        [JsonIgnore]
        public Court? Court { get; private set; } = null!;
        [JsonIgnore]
        public ICollection<User>? Users { get; private set; } = new List<User>();

        public Training() { }
    }
}
