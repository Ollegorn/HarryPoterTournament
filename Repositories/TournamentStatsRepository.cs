using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class TournamentStatsRepository : ITournamentStatsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TournamentStatsRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public Task<TournamentStats> AddTournamentStats(TournamentStats tournamentStats)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteTournamentStatsById(Guid tournamentStatsId)
        {
            var statsToDelete = await _dbContext.TournamentStats.FindAsync(tournamentStatsId);
            _dbContext.TournamentStats.Remove(statsToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TournamentStats>> GetAllTournamentStats()
        {
            var tournamentStatsList = await _dbContext.TournamentStats.ToListAsync();
            return tournamentStatsList;
        }

        public async Task<TournamentStats> GetTournamentStatsById(Guid id)
        {
            var tournamentStats = await _dbContext.TournamentStats.FirstOrDefaultAsync(ts => ts.Id == id);
            if (tournamentStats == null)
            {
                return null;
            }
            return tournamentStats;
        }

        public async Task<bool> UpdateTournamentStats(TournamentStats tournamentStats)
        {
            var tournamentStatsToUpdate = await _dbContext.TournamentStats.FindAsync(tournamentStats.Id);
            if (tournamentStatsToUpdate == null)
            {
                return false;
            }
            tournamentStatsToUpdate.TotalPoints = tournamentStats.TotalPoints;
            tournamentStatsToUpdate.Wins = tournamentStats.Wins;
            tournamentStatsToUpdate.Defeats = tournamentStats.Defeats;
            _dbContext.TournamentStats.Update(tournamentStatsToUpdate);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
