using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.Interfaces.TournamentStatsInterfaces;

namespace Services.TournamentStatsServices
{
    public class TournamentStatsGetterService : ITournamentStatsGetterService
    {
        private readonly ITournamentStatsRepository _tournamentStatsRepository;

        public TournamentStatsGetterService(ITournamentStatsRepository tournamentStatsRepository)
        {
            _tournamentStatsRepository = tournamentStatsRepository;
        }

        public async Task<List<TournamentStats>> GetAllTournamentStats()
        {
            var touranentStatsList = await _tournamentStatsRepository.GetAllTournamentStats();
            return touranentStatsList;
        }

        public async Task<TournamentStats> GetTournamentStatsById(Guid id)
        {
            var tournamentStats = await _tournamentStatsRepository.GetTournamentStatsById(id);
            
            if (tournamentStats == null)
            {
                return null;
            }
            return tournamentStats;
        }
    }
}
