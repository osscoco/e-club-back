using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Club.Application.DTOs.User.Response;
using E_Club.Application.DTOs.Users.Request;
using DomainModels.Entities;
using InfrastructureEFCore;
using Microsoft.EntityFrameworkCore;
using E_Club.Application.Interfaces.Users;

namespace E_Club.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        #region Attributs
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructeur
        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region GetUsers
        public async Task<IEnumerable<UserDtoResponse?>> GetUsers(CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().OrderBy(c => c.LastName).ProjectTo<UserDtoResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetUserById
        public async Task<UserDtoResponse?> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().ProjectTo<UserDtoResponse>(_mapper.ConfigurationProvider).Where(user => user.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        }
        #endregion

        #region CreateUser
        public async Task<User?> CreateUser(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);

            return await _context.SaveChangesAsync(cancellationToken) == 0 ? null : user;
        }
        #endregion

        #region UpdateUser
        public async Task<UserDtoResponse?> UpdateUser(Guid userId, UserDtoUpdateRequest userDtoUpdateRequest, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId, cancellationToken);

            if (user is null) return null;

            user.FirstName = userDtoUpdateRequest?.FirstName ?? string.Empty;
            user.LastName = userDtoUpdateRequest?.LastName ?? string.Empty;
            user.Age = userDtoUpdateRequest?.Age ?? 0;
            user.Email = userDtoUpdateRequest?.Email ?? string.Empty;
            user.PasswordHashed = userDtoUpdateRequest?.PasswordHashed ?? string.Empty;
            user.Phone = userDtoUpdateRequest?.Phone ?? string.Empty;
            user.ClubId = userDtoUpdateRequest?.ClubId ?? Guid.Empty;
            user.UserTypeId = userDtoUpdateRequest?.UserTypeId ?? Guid.Empty;

            return await _context.SaveChangesAsync(cancellationToken) == 0 ? null : _mapper.Map<UserDtoResponse>(user);
        }
        #endregion

        #region DeleteUserById
        public async Task<Guid?> DeleteUserById(Guid userId, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId, cancellationToken);

            if (user is null) return null;

            _context.Users.Remove(user);

            return await _context.SaveChangesAsync(cancellationToken) == 0 ? null : userId;
        }
        #endregion
    }
}
