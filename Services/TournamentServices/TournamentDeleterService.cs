using RepositoryContracts;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.TournamentInterfaces;
using ServiceContracts.Interfaces.TournamentStatsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TournamentServices
{
    public class TournamentDeleterService : ITournamentDeleterService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IDuelDeleterService _duelDeleterService;
        private readonly ITournamentStatsDeleterService _tournamentStatsDeleterService;

        public TournamentDeleterService(ITournamentRepository tournamentRepository, IDuelDeleterService duelDeleterService, ITournamentStatsDeleterService tournamentStatsDeleterService)
        {
            _tournamentRepository = tournamentRepository;
            _duelDeleterService = duelDeleterService;
            _tournamentStatsDeleterService = tournamentStatsDeleterService;
        }

        public async Task<bool> DeleteTournament(Guid id)
        {
            var tournamentToDelete = await _tournamentRepository.GetTournamentById(id);

            if (tournamentToDelete == null)
            {
                return false;
            }
            if (tournamentToDelete.TournamentDuels.Count != 0 )
            {

                foreach (var duel in tournamentToDelete.TournamentDuels)
                {
                    await _duelDeleterService.DeleteDuel(duel.DuelId);
                }
            }
            if (tournamentToDelete.RegisteredUsers.Count != 0)
            {
                var tournamentId = tournamentToDelete.TournamentId;

                foreach (var user in tournamentToDelete.RegisteredUsers)
                {
                    var tournamentStatsToDelete = user.TournamentStats.FirstOrDefault(ts => ts.TournamentId == tournamentId);

                    if (tournamentStatsToDelete != null)
                    {
                        await _tournamentStatsDeleterService.DeleteTournamentStatsById(tournamentStatsToDelete.Id);
                    }
                }
            }

            var tournamentResponse =tournamentToDelete.ToTournament();
            _tournamentRepository.DeleteTournament(tournamentResponse.TournamentId);
            return true;
        }
    }
}
