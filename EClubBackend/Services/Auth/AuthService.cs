using E_Club.Application.Interfaces.Common;
using DomainModels.Entities.Common;
using FluentValidation;
using E_Club.Application.Interfaces.Auth;
using E_Club.Application.Features.Auth.Login;
using E_Club.Application.Features.Auth.AuthMe;
using E_Club.Application.DTOs.Auth.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Club.Application.Features.Auth.Logout;
using E_Club.Services.Common.Security;
using DomainModels.Security;

namespace E_Club.Services.Auth
{
    public class AuthService : IAuthService
    {
        #region Attributs
        private readonly IValidator<LoginCommand> _validatorLogin;
        private readonly IPasswordHasher _passwordHasher;
        private readonly Token _token;
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly IMessageToReturn _messageToReturn;
        #endregion

        #region Constructeur
        public AuthService(
            IValidator<LoginCommand> validatorLogin,
            IPasswordHasher passwordHasher,
            Token token,
            IConfiguration configuration,
            IAuthRepository authRepository,
            IMessageToReturn messageToReturn
            )
        {
            _validatorLogin = validatorLogin;
            _passwordHasher = passwordHasher;
            _token = token;
            _configuration = configuration;
            _authRepository = authRepository;
            _messageToReturn = messageToReturn;
        }
        #endregion

        #region Login
        public async Task<ResponseApi<object>> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorLogin.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            LoginDtoResponse? user = await _authRepository.GetUserByEmail(request.loginDtoRequest.Email, cancellationToken);

            if (user == null)
                return new ResponseApi<object>(false, new { }, _messageToReturn.MessageError("user", "getById"));

            bool passwordOk = _passwordHasher.Verify(request.loginDtoRequest.PasswordHashed, user.PasswordHashed);

            if (!passwordOk)
                return new ResponseApi<object>(false, new { }, _messageToReturn.MessageError("user", "auth"));

            #region Generate Token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new ("ID", user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddMinutes(60), signingCredentials: signin);

            String userAndbearerToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponseApi<object>(true, userAndbearerToken, _messageToReturn.MessageSuccess("user", "auth"));
            #endregion
        }
        #endregion

        #region AuthMe
        public async Task<ResponseApi<object>> AuthMe(AuthMeCommand request, CancellationToken cancellationToken)
        {
            if (!request.user.Identity!.IsAuthenticated)
                return new ResponseApi<object>(false, new { }, _messageToReturn.MessageError("user", "auth"));

            var idClaim = request.user.FindFirst("ID")!.Value;

            if (string.IsNullOrWhiteSpace(idClaim) || !Guid.TryParse(idClaim, out var userId))
                return new ResponseApi<object>(false, new { }, _messageToReturn.MessageError("user", "getById"));

            var user = await _authRepository.GetUserById(userId, cancellationToken);

            if (user == null)
                return new ResponseApi<object>(false, new { }, _messageToReturn.MessageError("user", "getById"));

            return new ResponseApi<object>(true, user, _messageToReturn.MessageSuccess("user", "auth"));
        }
        #endregion

        #region Logout
        public ResponseApi<object> Logout(LogoutCommand request)
        {
            string token = request.HttpRequest.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
                return new ResponseApi<object>(false, new { }, "Aucun jeton fourni ...");

            _token.RevokeToken(token);

            return new ResponseApi<object>(true, new { }, "Vous êtes déconnecté !");
        }
        #endregion
    }
}
