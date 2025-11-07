namespace E_Club.Application.DTOs.Auth.Request
{
    public class LoginDtoRequest
    {
        public required string Email { get; set; }
        public required string PasswordHashed { get; set; }
    }
}
