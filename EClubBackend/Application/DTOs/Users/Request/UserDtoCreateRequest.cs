using DomainModels.Enums;

namespace E_Club.Application.DTOs.Users.Request
{
    public class UserDtoCreateRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Email { get; set; }
        public required string PasswordHashed { get; set; }
        public string? Phone { get; set; }

        // Mapping
        public required Guid UserTypeId { get; set; }
        public required Guid ClubId { get; set; }
    }
}
