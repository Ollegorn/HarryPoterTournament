using RepositoryContracts;
using ServiceContracts.Interfaces.TournamentStatsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TournamentStatsServices
{
    public class TournamentStatsDeleterService : ITournamentStatsDeleterService
    {
        private readonly ITournamentStatsRepository _tournStatsRepository;

        public TournamentStatsDeleterService(ITournamentStatsRepository tournStatsRepository)
        {
            _tournStatsRepository = tournStatsRepository;
        }

        public async Task<bool> DeleteTournamentStatsById(Guid tournamentStatsId)
        {
            var tournStatsToDelete = await _tournStatsRepository.DeleteTournamentStatsById(tournamentStatsId);

            if (tournStatsToDelete == null)
            {
                return false;
            }
            return true;
        }
    }
}
