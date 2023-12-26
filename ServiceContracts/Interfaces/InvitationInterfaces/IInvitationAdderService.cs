using Entities.Entities;
using ServiceContracts.InvitationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.InvitationInterfaces
{
    public interface IInvitationAdderService
    {
        Task<InvitationResponseDto> AddInvitation(InvitationAddRequestDto invitationAddRequestDto);
    }
}
