using ServiceContracts.TournamentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.TournamentInterfaces
{
    public interface ITournamentUpdaterService
    {
        Task<bool> UpdateTournament(TournamentUpdateRequestDto tournamentUpdateRequestDto);
        Task<bool> AddUserToTournament(Guid tournamnetId, string username);
        //Task<bool> RemoveUserFromTournament(Guid tournamentId , string username);
        Task<bool> StartTournament(Guid tournamentId);
    }
}
