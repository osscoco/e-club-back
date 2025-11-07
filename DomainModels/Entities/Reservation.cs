using DomainModels.Enums;
using System.Text.Json.Serialization;

namespace DomainModels.Entities
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public ReservationStatus? Status { get; set; } = ReservationStatus.PENDING;
        public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        public Guid? FirstPlayerId { get; set; }
        [JsonIgnore]
        public User? FirstPlayer { get; private set; } = null!;
        public Guid? SecondPlayerId { get; set; }
        [JsonIgnore]
        public User? SecondPlayer { get; private set; } = null!;
        public Guid? CourtId { get; set; }
        [JsonIgnore]
        public Court? Court { get; private set; } = null!;

        public Reservation() { }
    }
}
