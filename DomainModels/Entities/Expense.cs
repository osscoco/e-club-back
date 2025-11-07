using DomainModels.Enums;
using System.Text.Json.Serialization;

namespace DomainModels.Entities
{
    public class Expense
    {
        public Guid ExpenseId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Amount { get; set; }
        public required string Receiver { get; set; }
        public ExpenseCategory? Category { get; set; }
        public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        public Guid? UserCreatorId { get; set; }
        [JsonIgnore]
        public User? UserCreator { get; private set; } = null!;

        public Expense() { }
    }
}
