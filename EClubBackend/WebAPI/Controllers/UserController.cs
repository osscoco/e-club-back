using E_Club.Application.DTOs.Users.Request;
using E_Club.Application.Features.Users.CreateUser;
using E_Club.Application.Features.Users.DeleteUserById;
using E_Club.Application.Features.Users.GetUserById;
using E_Club.Application.Features.Users.GetUsers;
using E_Club.Application.Features.Users.UpdateUser;
using DomainModels.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace E_Club.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Attributs
        private readonly ISender _sender;
        #endregion

        #region Constructeur
        public UserController(ISender sender)
        {
            _sender = sender;
        }
        #endregion

        #region GetUsers
        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<ResponseApi<object>>> GetUsers()
        {
            ResponseApi<object> responseApi = await _sender.Send(new GetUsersQuery());
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region GetUserById
        // GET: api/User/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseApi<object>>> GetUserById(Guid userId)
        {
            ResponseApi<object> responseApi = await _sender.Send(new GetUserByIdQuery(userId));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region CreateUser
        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<ResponseApi<object>>> CreateUser(UserDtoCreateRequest userDtoCreateRequest)
        {
            ResponseApi<object> responseApi = await _sender.Send(new CreateUserCommand(userDtoCreateRequest));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region UpdateUser
        // PUT: api/User/{userId}
        [HttpPut("{userId}")]
        public async Task<ActionResult<ResponseApi<object>>> UpdateUser(Guid userId, UserDtoUpdateRequest userDtoUpdateRequest)
        {
            ResponseApi<object> responseApi = await _sender.Send(new UpdateUserCommand(userId, userDtoUpdateRequest));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region DeleteUser
        // DELETE: api/User/{userId}
        [HttpDelete("{userId}")]
        public async Task<ActionResult<ResponseApi<object>>> DeleteUser(Guid userId)
        {
            ResponseApi<object> responseApi = await _sender.Send(new DeleteUserByIdCommand(userId));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion
    }
}