using System.Text.Json.Serialization;

namespace DomainModels.Entities
{
    public class Club
    {
        public Guid ClubId { get; set; }
        public required string Name { get; set; }
        public required decimal CA { get; set; }
        public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        [JsonIgnore]
        public ICollection<Court>? Courts { get; private set; } = new List<Court>();
        [JsonIgnore]
        public ICollection<User>? Users { get; private set; } = new List<User>();

        public Club() { }
    }
}
