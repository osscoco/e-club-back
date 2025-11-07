using DomainModels.Enums;
using System.Text.Json.Serialization;

namespace DomainModels.Entities
{
    public class Income
    {
        public Guid IncomeId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Amount { get; set; }
        public required string Payer { get; set; }
        public IncomeCategory? Category { get; set; }
        public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        public Guid? UserCreatorId { get; set; }
        [JsonIgnore]
        public User? UserCreator { get; private set; } = null!;

        public Income() { }
    }
}
