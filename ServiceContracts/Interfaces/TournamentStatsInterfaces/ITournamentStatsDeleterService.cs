using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.TournamentStatsInterfaces
{
    public interface ITournamentStatsDeleterService
    {
        Task<bool> DeleteTournamentStatsById(Guid tournamentStatsId);
    }
}
