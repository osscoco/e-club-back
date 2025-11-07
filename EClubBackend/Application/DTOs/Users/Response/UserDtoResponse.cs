using DomainModels.Enums;
using E_Club.Application.DTOs.Clubs.Response;
using E_Club.Application.DTOs.UserTypes.Response;

namespace E_Club.Application.DTOs.User.Response
{
    public class UserDtoResponse
    {
        public Guid UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Mapping
        public required UserTypeDtoResponse UserType { get; set; }
        public required ClubDtoResponse Club { get; set; }
    }
}
