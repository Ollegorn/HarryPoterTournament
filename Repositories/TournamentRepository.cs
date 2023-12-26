using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.TournamentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserGetterService _userGetterService;

        public TournamentRepository(ApplicationDbContext applicationDbContext, IUserGetterService userGetterService)
        {
            _dbContext = applicationDbContext;
            _userGetterService = userGetterService;
        }

        public async Task<bool> AddUserToTournament(Guid tournamentId, string username)
        {
            var tournament = await _dbContext.Tournaments
                                        .Include(t => t.UserTournaments)
                                        .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);

            if (tournament == null)
            {
                return false;
            }

            var user = await _userGetterService.GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }

            if (tournament.UserTournaments.Any(ut => ut.UserId == user.Id))
            {
                return false;
            }

            var userTournament = new UserTournament
            {
                UserId = user.Id,
                TournamentId = tournamentId
            };

            tournament.UserTournaments.Add(userTournament);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TournamentResponseDto>> GetAllTournaments()
        {
            var tournaments = await _dbContext.Tournaments
                .Include(t => t.UserTournaments)
                    .ThenInclude(ut => ut.User)
                        .ThenInclude(u => u.TournamentStats)
                .Include(t => t.TournamentDuels)
                .ToListAsync();

            var tournamentResponseDto = tournaments.Select(t => t.ToTournamentResponseDto()).ToList();

            return tournamentResponseDto;
        }

        public async Task<TournamentResponseDto> GetTournamentById(Guid id)
        {
            var tournament = await _dbContext.Tournaments
                .Include(t => t.UserTournaments)
                    .ThenInclude(ut => ut.User)
                        .ThenInclude(u => u.TournamentStats)
                .Include(t => t.TournamentDuels)
                .FirstOrDefaultAsync(t => t.TournamentId == id);

            if (tournament == null)
            {
                return null;
            }

            var tournamentResponseDto = tournament.ToTournamentResponseDto();

            return tournamentResponseDto;
        }

        public async Task<List<User>> GetRegisteredUsersForTournament(Guid tournamentId)
        {
            return await _dbContext.UserTournaments
                .Where(ut => ut.TournamentId == tournamentId)
                .Include(ut => ut.User.TournamentStats)
                .Select(ut => ut.User)
                .ToListAsync();
        }

        public async Task<Tournament> AddTournament(Tournament tournament)
        {
            _dbContext.Add(tournament);
            await _dbContext.SaveChangesAsync();
            return tournament;
        }

        public async Task<bool> UpdateTournament(Tournament tournament)
        {
            var tournamentToUpdate = await _dbContext.Tournaments
                .Include(t => t.UserTournaments)
                .FirstOrDefaultAsync(t => t.TournamentId == tournament.TournamentId);

            if (tournamentToUpdate == null)
            {
                return false;
            }

            tournamentToUpdate.TournamentName = tournament.TournamentName;
            tournamentToUpdate.Prize = tournament.Prize;
            tournamentToUpdate.Rules = tournament.Rules;

            // Clear existing UserTournaments and add the new ones
            tournamentToUpdate.UserTournaments.Clear();

            foreach (var userTournamentDto in tournament.UserTournaments)
            {
                var userTournament = new UserTournament
                {
                    UserId = userTournamentDto.UserId,
                    TournamentId = tournament.TournamentId
                };

                tournamentToUpdate.UserTournaments.Add(userTournament);
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTournament(Guid tournamentId)
        {
            var tournamentToDelete = await _dbContext.Tournaments.FindAsync(tournamentId);
            _dbContext.Tournaments.Remove(tournamentToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TournamentResponseDto> GetTournamentByDuelId(Guid duelId)
        {
            var tournaments = await _dbContext.Tournaments
                .Include(t => t.TournamentDuels)
                .ToListAsync();

            foreach (var tournament in tournaments)
            {
                if (tournament.TournamentDuels.Any(d => d.DuelId == duelId))
                {
                    var tournamentResponse = tournament.ToTournamentResponseDto();
                    
                    return tournamentResponse;
                }
            }

            return null;
        }

        public async Task<bool> RemoveUserFromTournament(Guid tournamentId, string username)
        {
            var tournament = await _dbContext.Tournaments
                                        .Include(t => t.UserTournaments)
                                        .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);

            if (tournament == null)
            {
                return false;
            }

            var user = await _userGetterService.GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }

            var userTournament = tournament.UserTournaments.FirstOrDefault(ut => ut.UserId == user.Id);

            if (userTournament == null)
            {
                return false;
            }

            tournament.UserTournaments.Remove(userTournament);

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
