using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.InvitationInterfaces
{
    public interface IInvitationDeleterService
    {
        Task<bool> DeleteInvitationById(Guid id);
        Task<bool> DeleteInvitationByTournamentId(Guid tournamentId);
    }
}
