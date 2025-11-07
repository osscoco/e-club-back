using DomainModels.Entities;

namespace E_Club.Application.DTOs.Auth.Response
{
    public class AuthMeDtoResponse
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Email { get; set; }
        public required UserType UserType { get; set; }
        public required Club Club { get; set; }
        public string? Phone { get; set; }
    }
}
