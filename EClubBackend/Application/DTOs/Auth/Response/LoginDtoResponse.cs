namespace E_Club.Application.DTOs.Auth.Response
{
    public class LoginDtoResponse
    {
        public Guid UserId { get; set; }
        public required string Email { get; set; }
        public required string PasswordHashed { get; set; }
    }
}
