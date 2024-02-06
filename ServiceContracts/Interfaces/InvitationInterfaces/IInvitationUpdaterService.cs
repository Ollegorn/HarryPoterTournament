using ServiceContracts.InvitationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.InvitationInterfaces
{
    public interface IInvitationUpdaterService
    {
        Task<bool> ReturnInvitationToSender(InvitationUpdateRequestDto invitationUpdateRequestDto);

        Task<bool> AcceptInvitation(InvitationUpdateRequestDto invitationUpdateRequestDto);
    }
}
