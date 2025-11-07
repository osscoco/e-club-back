using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Club.Application.DTOs.Clubs.Request;
using E_Club.Application.DTOs.Clubs.Response;
using E_Club.Application.Interfaces.Clubs;
using DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
using InfrastructureEFCore;

namespace E_Club.Persistence.Repositories.Clubs
{
    public class ClubRepository : IClubRepository
    {
        #region Attributs
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructeur
        public ClubRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region GetClubs
        public async Task<IEnumerable<ClubDtoResponse?>> GetClubs(CancellationToken cancellationToken)
        {
            return await _context.Clubs.AsNoTracking().OrderBy(c => c.Name).ProjectTo<ClubDtoResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetClubById
        public async Task<ClubDtoResponse?> GetClubById(Guid clubId, CancellationToken cancellationToken)
        {
            return await _context.Clubs.AsNoTracking().ProjectTo<ClubDtoResponse>(_mapper.ConfigurationProvider).Where(club => club.ClubId == clubId).FirstOrDefaultAsync(cancellationToken);
        }
        #endregion

        #region CreateClub
        public async Task<Club?> CreateClub(Club club, CancellationToken cancellationToken)
        {
            _context.Clubs.Add(club);

            return await _context.SaveChangesAsync(cancellationToken) == 0 ? null : club;
        }
        #endregion

        #region UpdateClub
        public async Task<ClubDtoResponse?> UpdateClub(Guid clubId, ClubDtoUpdateRequest clubDtoUpdateRequest, CancellationToken cancellationToken)
        {
            Club? club = await _context.Clubs.FirstOrDefaultAsync(c => c.ClubId == clubId, cancellationToken);
            
            if (club is null) return null;

            club.Name = clubDtoUpdateRequest?.Name ?? string.Empty;
            club.CA = clubDtoUpdateRequest?.CA ?? 0;

            return await _context.SaveChangesAsync(cancellationToken) == 0 ? null : _mapper.Map<ClubDtoResponse>(club);
        }
        #endregion

        #region DeleteClubById
        public async Task<Guid?> DeleteClubById(Guid clubId, CancellationToken cancellationToken)
        {
            Club? club = await _context.Clubs.FirstOrDefaultAsync(c => c.ClubId == clubId, cancellationToken);

            if (club is null) return null;

            _context.Clubs.Remove(club);

            return await _context.SaveChangesAsync(cancellationToken) == 0 ? null : clubId;
        }
        #endregion
    }
}