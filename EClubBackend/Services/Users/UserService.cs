using DomainModels.Entities.Common;
using DomainModels.Entities;
using E_Club.Application.Interfaces.Common;
using FluentValidation;
using E_Club.Application.Features.Users.GetUserById;
using E_Club.Application.Features.Users.CreateUser;
using E_Club.Application.Features.Users.UpdateUser;
using E_Club.Application.Features.Users.DeleteUserById;
using E_Club.Application.Interfaces.Users;
using E_Club.Application.DTOs.User.Response;
using DomainModels.Security;

namespace E_Club.Services.Users
{
    public class UserService : IUserService
    {
        #region Attributs
        private readonly IValidator<GetUserByIdQuery> _validatorGetUserById;
        private readonly IValidator<CreateUserCommand> _validatorCreateUser;
        private readonly IValidator<UpdateUserCommand> _validatorUpdateUser;
        private readonly IValidator<DeleteUserByIdCommand> _validatorDeleteUserById;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMessageToReturn _messageToReturn;
        private readonly IRedisCache _redisCache;
        #endregion

        #region Constructeur
        public UserService(
            IValidator<GetUserByIdQuery> validatorGetUserById,
            IValidator<CreateUserCommand> validatorCreateUser,
            IValidator<UpdateUserCommand> validatorUpdateUser,
            IValidator<DeleteUserByIdCommand> validatorDeleteUserById,
            IPasswordHasher passwordHasher,
            IUserRepository userRepository,
            IMessageToReturn messageToReturn,
            IRedisCache redisCache
            )
        {
            _validatorGetUserById = validatorGetUserById;
            _validatorCreateUser = validatorCreateUser;
            _validatorUpdateUser = validatorUpdateUser;
            _validatorDeleteUserById = validatorDeleteUserById;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _messageToReturn = messageToReturn;
            _redisCache = redisCache;
        }
        #endregion

        #region GetUsers
        public async Task<ResponseApi<object>> GetUsers(CancellationToken cancellationToken)
        {
            var users = _redisCache.GetData<IEnumerable<UserDtoResponse?>>("users");

            if (users is not null)
                return new ResponseApi<object>(true, users, _messageToReturn.MessageSuccess("user", "get"));

            users = await _userRepository.GetUsers(cancellationToken);

            if (users.Count() == 0)
                return new ResponseApi<object>(true, users, _messageToReturn.MessageError("user", "get"));

            _redisCache.SetData("users", users);

            return new ResponseApi<object>(true, users, _messageToReturn.MessageSuccess("user", "get"));
        }
        #endregion

        #region GetUserById
        public async Task<ResponseApi<object>> GetUserById(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorGetUserById.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            var user = _redisCache.GetData<UserDtoResponse?>("user_" + query.userId);

            if (user is not null)
                return new ResponseApi<object>(true, user, _messageToReturn.MessageSuccess("user", "getById"));

            user = await _userRepository.GetUserById(query.userId, cancellationToken);

            if (user is null)
                return new ResponseApi<object>(true, new { }, _messageToReturn.MessageError("user", "getById"));

            _redisCache.SetData("user_" + query.userId, user);

            return new ResponseApi<object>(true, user, _messageToReturn.MessageSuccess("user", "getById"));
        }
        #endregion

        #region CreateUser
        public async Task<ResponseApi<object>> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorCreateUser.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            var user = new User
            {
                UserId = Guid.NewGuid(),
                FirstName = command.userDtoCreateRequest.FirstName,
                LastName = command.userDtoCreateRequest.LastName,
                Age = command.userDtoCreateRequest.Age,
                Email = command.userDtoCreateRequest.Email,
                PasswordHashed = _passwordHasher.Hash(command.userDtoCreateRequest.PasswordHashed),
                Phone = command.userDtoCreateRequest.Phone,
                LastUpdatedDate = null,
                DeletedDate = null,
                UserTypeId = command.userDtoCreateRequest.UserTypeId,
                ClubId = command.userDtoCreateRequest.ClubId
            };

            var userAdded = await _userRepository.CreateUser(user, cancellationToken);

            #region Traitement RedisCache
            var userInCache = _redisCache.GetData<UserDtoResponse?>("user_" + userAdded!.UserId);

            if (userInCache is not null)
                _redisCache.RemoveData<UserDtoResponse?>("user_" + userInCache!.UserId);

            _redisCache.RemoveData<IEnumerable<UserDtoResponse?>>("users");
            #endregion

            return new ResponseApi<object>(true, userInCache, _messageToReturn.MessageSuccess("user", "create"));
        }
        #endregion

        #region UpdateUser
        public async Task<ResponseApi<object>> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorUpdateUser.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            var userUpdated = await _userRepository.UpdateUser(command.userId, command.userDtoUpdateRequest, cancellationToken);

            if (userUpdated is null)
                return new ResponseApi<object>(true, new { }, _messageToReturn.MessageError("user", "update"));

            #region Traitement RedisCache
            var userInCache = _redisCache.GetData<UserDtoResponse?>("user_" + userUpdated!.UserId);

            if (userInCache is not null)
                _redisCache.RemoveData<UserDtoResponse?>("user_" + userUpdated!.UserId);

            _redisCache.RemoveData<IEnumerable<UserDtoResponse?>>("users");
            #endregion

            return new ResponseApi<object>(true, userUpdated, _messageToReturn.MessageSuccess("user", "update"));
        }
        #endregion

        #region DeleteUserById
        public async Task<ResponseApi<object>> DeleteUserById(DeleteUserByIdCommand command, CancellationToken cancellationToken)
        {
            #region FluentValidation
            var validationResult = await _validatorDeleteUserById.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { PropertyName = e.PropertyName, ErrorMessage = e.ErrorMessage }).ToList();
                return new ResponseApi<object>(false, errors, "Champs invalides");
            }
            #endregion

            Guid? userIdDeleted = await _userRepository.DeleteUserById(command.userId, cancellationToken);

            if (userIdDeleted is null)
                return new ResponseApi<object>(true, new { }, _messageToReturn.MessageError("user", "delete"));

            #region Traitement RedisCache
            var userInCache = _redisCache.GetData<UserDtoResponse?>("user_" + userIdDeleted);

            if (userInCache is not null)
                _redisCache.RemoveData<UserDtoResponse?>("user_" + userIdDeleted);

            _redisCache.RemoveData<IEnumerable<UserDtoResponse?>>("users");
            #endregion

            return new ResponseApi<object>(true, userIdDeleted, _messageToReturn.MessageSuccess("user", "delete"));
        }
        #endregion
    }
}
