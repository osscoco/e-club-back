using DomainModels.Enums;

namespace E_Club.Application.DTOs.Users.Request
{
    public class UserDtoUpdateRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? PasswordHashed { get; set; }
        public string? Phone { get; set; }

        // Mapping
        public Guid? UserTypeId { get; set; }
        public Guid? ClubId { get; set; }
    }
}
