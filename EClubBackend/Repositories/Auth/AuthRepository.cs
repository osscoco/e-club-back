using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Club.Application.DTOs.Auth.Response;
using E_Club.Application.Interfaces.Auth;
using InfrastructureEFCore;
using Microsoft.EntityFrameworkCore;

namespace E_Club.Persistence.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        #region Attributs
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructeur
        public AuthRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region GetUserByEmail
        public async Task<LoginDtoResponse?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().ProjectTo<LoginDtoResponse>(_mapper.ConfigurationProvider).Where(user => user.Email == email).FirstOrDefaultAsync(cancellationToken);
        }
        #endregion

        #region GetUserById
        public async Task<AuthMeDtoResponse?> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().Where(u => u.UserId == userId).ProjectTo<AuthMeDtoResponse>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        }
        #endregion
    }
}