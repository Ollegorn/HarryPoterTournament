using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.TournamentInterfaces
{
    public interface ITournamentDeleterService
    {
        Task<bool> DeleteTournament(Guid id);
    }
}
