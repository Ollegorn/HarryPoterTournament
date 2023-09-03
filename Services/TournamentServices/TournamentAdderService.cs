using Entities.Entities;
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
    public class TournamentAdderService : ITournamentAdderService
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentAdderService(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<Tournament> AddTournament(TournamentAddRequestDto tournamentAddRequestDto)
        {
            var tournament = tournamentAddRequestDto.ToTournament();

            var addedTournament = await _tournamentRepository.AddTournament(tournament);

            return addedTournament;
        }
    }
}
