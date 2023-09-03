using Entities.Entities;
using ServiceContracts.TournamentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface ITournamentRepository
    {
        Task<List<TournamentResponseDto>> GetAllTournaments();
        
        Task<TournamentResponseDto> GetTournamentById(Guid id);

        Task<Tournament> AddTournament(Tournament tournament);

        Task<bool> DeleteTournament(Guid tournamentId);

        Task<bool> UpdateTournament(Tournament tournament);

        Task<bool> AddUserToTournament(Guid tournamentId, string username);

        Task<List<User>> GetRegisteredUsersForTournament(Guid tournamentId);
    }
}
