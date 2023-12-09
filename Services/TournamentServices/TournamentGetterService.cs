using RepositoryContracts;
using ServiceContracts.Interfaces.TournamentInterfaces;
using ServiceContracts.TournamentDto;

namespace Services.TournamentServices
{
    public class TournamentGetterService : ITournamentGetterService
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentGetterService(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentResponseDto> GetTournamentById(Guid id)
        {
            var tournament = await _tournamentRepository.GetTournamentById(id);

            return tournament;
        }

        public async Task<List<TournamentResponseDto>> GetAllTournaments()
        {
            var tournaments = await _tournamentRepository.GetAllTournaments();

            return tournaments;
        }

        public async Task<TournamentResponseDto> GetTournamentByDuelId(Guid duelId)
        {
            var tournament = await _tournamentRepository.GetTournamentByDuelId(duelId);

            if(tournament == null)
            {
                return null;
            }
            return tournament;
        }
    }
}
