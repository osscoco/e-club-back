using System.Text.Json.Serialization;

namespace DomainModels.Entities
{
    public class UserType
    {
        public Guid UserTypeId { get; set; }
        public required string Name { get; set; }
        public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        [JsonIgnore]
        public ICollection<User>? Users { get; private set; } = new List<User>();

        public UserType() { }
    }
}
