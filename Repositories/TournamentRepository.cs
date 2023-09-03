using Entities;
using Entities.Entities;
using Entities.Migrations;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.TournamentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var tournament = await _dbContext.Tournaments.Include(t => t.RegisteredUsers).FirstOrDefaultAsync(t => t.TournamentId == tournamentId);

            if (tournament == null)
            {
                return false; 
            }

            var user = await _userGetterService.GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }

            if (tournament.RegisteredUsers.Contains(user))
            {
                return false;
            }

            tournament.RegisteredUsers.Add(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Tournament> AddTournament(Tournament tournament)
        {
            _dbContext.Add(tournament);
            await _dbContext.SaveChangesAsync();
            return tournament;
        }

        public async Task<bool> DeleteTournament(Guid tournamentId)
        {
            var tournamentToDelete = await _dbContext.Tournaments.FindAsync(tournamentId);
             _dbContext.Tournaments.Remove(tournamentToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TournamentResponseDto>> GetAllTournaments()
        {
            var tournaments = await _dbContext.Tournaments
                .Include(t => t.RegisteredUsers)
                .Include(t => t.TournamentDuels)
                .ToListAsync();

            var tournamentResponseDto = tournaments.Select(t => t.ToTournamentResponseDto()).ToList();

            return tournamentResponseDto;
        }

        public async Task<TournamentResponseDto> GetTournamentById(Guid id)
        {
            var tournament = await _dbContext.Tournaments
                .Include(t => t.RegisteredUsers)
                .Include(t => t.TournamentDuels)
                .FirstOrDefaultAsync(t => t.TournamentId == id);
            
            if (tournament == null)
            {
                return null;
            }

            var tournamentResponseDto = tournament.ToTournamentResponseDto();

            return tournamentResponseDto;
        }

        public async Task<bool> UpdateTournament(Tournament tournament)
        {
            var tournamentToUpdate = await _dbContext.Tournaments
                .Include(t => t.RegisteredUsers)
                .FirstOrDefaultAsync(t => t.TournamentId == tournament.TournamentId);
            if (tournamentToUpdate == null)
            {
                return false;
            }

            tournamentToUpdate.TournamentName = tournament.TournamentName;
            tournamentToUpdate.Prize = tournament.Prize;
            tournamentToUpdate.Rules = tournament.Rules;

            tournamentToUpdate.RegisteredUsers = tournament.RegisteredUsers;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<User>> GetRegisteredUsersForTournament(Guid tournamentId)
        {
            return await _dbContext.Tournaments
                .Where(t => t.TournamentId == tournamentId)
                .SelectMany(t => t.RegisteredUsers)
                .ToListAsync();
        }
    }
}
