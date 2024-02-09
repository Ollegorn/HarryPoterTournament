using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.Interfaces.InvitationInterfaces;
using ServiceContracts.InvitationDto;

namespace Services.InvitationServices
{
    public class InvitationGetterService : IInvitationGetterService
    {
        private readonly IInvitationRepository _invitationRepository;

        public InvitationGetterService(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task<List<InvitationResponseDto>> GetAllInvitations()
        {
            var invitations = await _invitationRepository.GetAllInvitations();
            return invitations;
        }

        public async Task<InvitationResponseDto> GetInvitationById(Guid id)
        {
            var invitation = await _invitationRepository.GetInvitationById(id);
            return invitation;
        }
        public async Task<List<InvitationResponseDto>> GetInvitationsByDuelId(Guid duelId)
        {
            var invitations = await _invitationRepository.GetInvitationsByDuelId(duelId);
            return invitations;
        }
    }
}
