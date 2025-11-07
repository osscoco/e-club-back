using E_Club.Application.DTOs.Auth.Request;
using E_Club.Application.Features.Auth.AuthMe;
using E_Club.Application.Features.Auth.Login;
using E_Club.Application.Features.Auth.Logout;
using DomainModels.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Club.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Attributs
        private readonly ISender _sender;
        #endregion

        #region Constructeur
        public AuthController(ISender sender)
        {
            _sender = sender;
        }
        #endregion

        #region Login
        // POST: api/Auth/login
        [HttpPost("login")]
        [AllowAnonymous] 
        public async Task<ActionResult<ResponseApi<object>>> Login(LoginDtoRequest loginDtoRequest)
        {
            ResponseApi<object> responseApi = await _sender.Send(new LoginCommand(loginDtoRequest));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region AuthMe
        // GET: api/Auth/authMe
        [HttpGet("authMe")]
        public async Task<ActionResult<ResponseApi<object>>> AuthMe()
        {
            ResponseApi<object> responseApi = await _sender.Send(new AuthMeCommand(HttpContext.User));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region Logout
        // GET: api/Auth/logout
        [HttpGet("logout")]
        public async Task<ActionResult<ResponseApi<object>>> Logout()
        {
            ResponseApi<object> responseApi = await _sender.Send(new LogoutCommand(Request));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion
    }
}