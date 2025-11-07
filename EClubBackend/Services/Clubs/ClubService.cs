using E_Club.Application.DTOs.Clubs.Response;
using E_Club.Application.Features.Clubs.CreateClub;
using E_Club.Application.Features.Clubs.DeleteClubById;
using E_Club.Application.Features.Clubs.GetClubById;
using E_Club.Application.Features.Clubs.UpdateClub;
using E_Club.Application.Interfaces.Clubs;
using E_Club.Application.Interfaces.Common;
using DomainModels.Entities;
using DomainModels.Entities.Common;
using FluentValidation;

namespace E_Club.Services.Clubs
{
    public class ClubService : IClubService
    {
        #region Attributs
        private readonly IValidator<GetClubByIdQuery> _validatorGetClubById;
        private readonly IValidator<CreateClubCommand> _validatorCreateClub;
        private readonly IValidator<UpdateClubCommand> _validatorUpdateClub;
        private readonly IValidator<DeleteClubByIdCommand> _validatorDeleteClubById;
        private readonly IClubRepository _clubRepository;
        private readonly IMessageToReturn _messageToReturn;
        private readonly IRedisCache _redisCache;
        #endregion

        #region Constructeur
        public ClubService(
            IValidator<GetClubByIdQuery> validatorGetClubById,
            IValidator<CreateClubCommand> validatorCreateClub,
            IValidator<UpdateClubCommand> validatorUpdateClub,
            IValidator<DeleteClubByIdCommand> validatorDeleteClubById,
            IClubRepository clubRepository,
            IMessageToReturn messageToReturn,
            IRedisCache redisCache
            )
        {
            _validatorGetClubById = validatorGetClubById;
            _validatorCreateClub = validatorCreateClub;
            _validatorUpdateClub = validatorUpdateClub;
            _validatorDeleteClubById = validatorDeleteClubById;
            _clubRepository = clubRepository;
            _messageToReturn = messageToReturn;
            _redisCache = redisCache;
        }
        #endregion

        #region GetClubs
        public async Task<ResponseApi<object>> GetClubs(CancellationToken cancellationToken)
        {
            var clubs = _redisCache.GetData<IEnumerable<ClubDtoResponse?>>("clubs");

            if (clubs is not null)
                return new ResponseApi<object>(true, clubs, _messageToReturn.MessageSuccess("club", "get"));

            clubs = await _clubRepository.GetClubs(cancellationToken);

            if (clubs.Count() == 0)
                return new ResponseApi<object>(true, clubs, _messageToReturn.MessageError("club", "get"));

            _redisCache.SetData("clubs", clubs);

            return new ResponseApi<object>(true, clubs, _messageToReturn.MessageSuccess("club", "get"));
        }
        #endregion

        #region GetClubById
        public async Task<ResponseApi<object>> GetClubById(GetClubByIdQuery query, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorGetClubById.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            var club = _redisCache.GetData<ClubDtoResponse?>("club_" + query.clubId);

            if (club is not null)
                return new ResponseApi<object>(true, club, _messageToReturn.MessageSuccess("club", "getById"));

            club = await _clubRepository.GetClubById(query.clubId, cancellationToken);

            if (club is null)
                return new ResponseApi<object>(true, new { }, _messageToReturn.MessageError("club", "getById"));

            _redisCache.SetData("club_" + query.clubId, club);

            return new ResponseApi<object>(true, club, _messageToReturn.MessageSuccess("club", "getById"));
        }
        #endregion

        #region CreateClub
        public async Task<ResponseApi<object>> CreateClub(CreateClubCommand command, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorCreateClub.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            var club = new Club
            {
                ClubId = Guid.NewGuid(),
                Name = command.clubDtoCreateRequest.Name,
                CA = command.clubDtoCreateRequest.CA,
                LastUpdatedDate = null,
                DeletedDate = null
            };

            var clubAdded = await _clubRepository.CreateClub(club, cancellationToken);

            #region Traitement RedisCache
            var clubInCache = _redisCache.GetData<ClubDtoResponse?>("club_" + clubAdded!.ClubId);

            if (clubInCache is not null)
                _redisCache.RemoveData<ClubDtoResponse?>("club_" + clubAdded!.ClubId);

            _redisCache.RemoveData<IEnumerable<ClubDtoResponse?>>("clubs");
            #endregion

            return new ResponseApi<object>(true, clubAdded, _messageToReturn.MessageSuccess("club", "create"));
        }
        #endregion

        #region UpdateClub
        public async Task<ResponseApi<object>> UpdateClub(UpdateClubCommand command, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorUpdateClub.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            var clubUpdated = await _clubRepository.UpdateClub(command.clubId, command.clubDtoUpdateRequest, cancellationToken);

            if (clubUpdated is null)
                return new ResponseApi<object>(true, new { }, _messageToReturn.MessageError("club", "update"));

            #region Traitement RedisCache
            var clubInCache = _redisCache.GetData<ClubDtoResponse?>("club_" + clubUpdated!.ClubId);

            if (clubInCache is not null)
                _redisCache.RemoveData<ClubDtoResponse?>("club_" + clubUpdated!.ClubId);

            _redisCache.RemoveData<IEnumerable<ClubDtoResponse?>>("clubs");
            #endregion

            return new ResponseApi<object>(true, clubUpdated, _messageToReturn.MessageSuccess("club", "update"));
        }
        #endregion

        #region DeleteClubById
        public async Task<ResponseApi<object>> DeleteClubById(DeleteClubByIdCommand command, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorDeleteClubById.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            Guid? clubIdDeleted = await _clubRepository.DeleteClubById(command.clubId, cancellationToken);

            if (clubIdDeleted is null)
                return new ResponseApi<object>(true, new { }, _messageToReturn.MessageError("club", "delete"));

            #region Traitement RedisCache
            var clubInCache = _redisCache.GetData<ClubDtoResponse?>("club_" + clubIdDeleted);

            if (clubInCache is not null)
                _redisCache.RemoveData<ClubDtoResponse?>("club_" + clubIdDeleted);

            _redisCache.RemoveData<IEnumerable<ClubDtoResponse?>>("clubs");
            #endregion

            return new ResponseApi<object>(true, clubIdDeleted, _messageToReturn.MessageSuccess("club", "delete"));
        }
        #endregion
    }
}