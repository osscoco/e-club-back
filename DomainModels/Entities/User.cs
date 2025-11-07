using System.Text.Json.Serialization;

namespace DomainModels.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Email { get; set; }
        public required string PasswordHashed { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        public required Guid UserTypeId { get; set; }
        [JsonIgnore]
        public UserType? UserType { get; private set; }
        public required Guid ClubId { get; set; }
        [JsonIgnore]
        public Club? Club { get; private set; }
        [JsonIgnore]
        public ICollection<Training>? Trainings { get; private set; } = new List<Training>();
        [JsonIgnore]
        public ICollection<Income>? Incomes { get; private set; } = new List<Income>();
        [JsonIgnore]
        public ICollection<Expense>? Expenses { get; private set; } = new List<Expense>();
        [JsonIgnore]
        public ICollection<Reservation>? ReservationsFirstPlayer { get; private set; } = new List<Reservation>();
        [JsonIgnore]
        public ICollection<Reservation>? ReservationsSecondPlayer { get; private set; } = new List<Reservation>();

        public User() { }
    }
}
