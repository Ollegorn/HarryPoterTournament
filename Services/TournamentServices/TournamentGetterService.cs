using RepositoryContracts;
using ServiceContracts.Interfaces.TournamentInterfaces;
using ServiceContracts.TournamentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
