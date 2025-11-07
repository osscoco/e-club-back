using E_Club.Application.DTOs.Clubs.Request;
using E_Club.Application.Features.Clubs.CreateClub;
using E_Club.Application.Features.Clubs.DeleteClubById;
using E_Club.Application.Features.Clubs.GetClubById;
using E_Club.Application.Features.Clubs.GetClubs;
using E_Club.Application.Features.Clubs.UpdateClub;
using DomainModels.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace E_Club.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        #region Attributs
        private readonly ISender _sender;
        #endregion

        #region Constructeur
        public ClubController(ISender sender)
        {
            _sender = sender;
        }
        #endregion

        #region GetClubs
        // GET: api/Club
        [HttpGet]
        public async Task<ActionResult<ResponseApi<object>>> GetClubs()
        {
            ResponseApi<object> responseApi = await _sender.Send(new GetClubsQuery());
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region GetClubById
        // GET: api/Club/{clubId}
        [HttpGet("{clubId}")]
        public async Task<ActionResult<ResponseApi<object>>> GetClubById(Guid clubId)
        {
            ResponseApi<object> responseApi = await _sender.Send(new GetClubByIdQuery(clubId));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region CreateClub
        // POST: api/Club
        [HttpPost]
        public async Task<ActionResult<ResponseApi<object>>> CreateClub(ClubDtoCreateRequest clubDtoCreateRequest)
        {
            ResponseApi<object> responseApi = await _sender.Send(new CreateClubCommand(clubDtoCreateRequest));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region UpdateClub
        // PUT: api/Club/{clubId}
        [HttpPut("{clubId}")]
        public async Task<ActionResult<ResponseApi<object>>> UpdateClub(Guid clubId, ClubDtoUpdateRequest clubDtoUpdateRequest)
        {
            ResponseApi<object> responseApi = await _sender.Send(new UpdateClubCommand(clubId, clubDtoUpdateRequest));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion

        #region DeleteClub
        // DELETE: api/Club/{clubId}
        [HttpDelete("{clubId}")]
        public async Task<ActionResult<ResponseApi<object>>> DeleteClub(Guid clubId)
        {
            ResponseApi<object> responseApi = await _sender.Send(new DeleteClubByIdCommand(clubId));
            if (responseApi.Data is null)
                return NotFound();

            return Ok(responseApi);
        }
        #endregion
    }
}