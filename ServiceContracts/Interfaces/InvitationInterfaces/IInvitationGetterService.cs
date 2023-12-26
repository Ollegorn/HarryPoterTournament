using Entities.Entities;
using ServiceContracts.InvitationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.InvitationInterfaces
{
    public interface IInvitationGetterService
    {
        Task<List<InvitationResponseDto>> GetAllInvitations();
        Task<InvitationResponseDto> GetInvitationById(Guid id);
    }
}
