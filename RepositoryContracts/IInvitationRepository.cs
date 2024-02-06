using Entities.Entities;
using ServiceContracts.InvitationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IInvitationRepository
    {
        Task<List<InvitationResponseDto>> GetAllInvitations();
        Task<InvitationResponseDto> GetInvitationById(Guid id);
        Task<Invitation> AddInvitation(Invitation invitation);
        Task<bool> DeleteInvitationById(Guid id);
        Task<bool> UpdateInvitation(InvitationUpdateRequestDto invitationUpdateRequestDto);
    }
}
