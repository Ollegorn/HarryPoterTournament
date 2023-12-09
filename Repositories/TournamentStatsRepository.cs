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

        public Task<bool> DeleteTournamentStatsById(Guid id)
        {
            throw new NotImplementedException();
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

        public Task<bool> UpdateTournamentStats(TournamentStats tournamentStats)
        {
            throw new NotImplementedException();
        }
    }
}
