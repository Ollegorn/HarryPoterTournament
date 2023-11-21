using RepositoryContracts;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.TournamentInterfaces;
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

        public TournamentDeleterService(ITournamentRepository tournamentRepository,IDuelDeleterService duelDeleterService)
        {
            _tournamentRepository = tournamentRepository;
            _duelDeleterService = duelDeleterService;
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

            var tournamentResponse =tournamentToDelete.ToTournament();
            _tournamentRepository.DeleteTournament(tournamentResponse.TournamentId);
            return true;
        }
    }
}
