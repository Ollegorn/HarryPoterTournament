using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.InvitationInterfaces;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.InvitationDto;

namespace Services.InvitationServices
{
    public class InvitationAdderService : IInvitationAdderService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUserGetterService _userGetterService;
        private readonly IUserRepository _userRepository;
        private readonly IDuelGetterService _duelGetterService;
        private readonly IDuelUpdaterService _duelUpdaterService;
        public InvitationAdderService(IInvitationRepository invitationRepository, IUserGetterService userGetterService, IUserRepository userRepository, IDuelGetterService duelGetterService, IDuelUpdaterService duelUpdaterService)
        {
            _invitationRepository = invitationRepository;
            _userGetterService = userGetterService;
            _userRepository = userRepository;
            _duelGetterService = duelGetterService;
            _duelUpdaterService = duelUpdaterService;
        }

        public async Task<InvitationResponseDto> AddInvitation(InvitationAddRequestDto invitationAddRequestDto)
        {
            var sender = await _userGetterService.GetUserByUsername(invitationAddRequestDto.SenderUsername);
            var recipient = await _userGetterService.GetUserByUsername(invitationAddRequestDto.RecipientUsername);

            var invitation = invitationAddRequestDto.ToInvitation(sender, recipient);

            var duelResponseDto = await _duelGetterService.GetDuelById(invitationAddRequestDto.DuelId);

            var duelUpdate = new DuelUpdateRequestDto
            {
                DuelId = duelResponseDto.DuelId,
                isChallenged = true,
            };

            await _duelUpdaterService.UpdateDuel(duelUpdate);

            
            recipient.ReceivedInvitations.Add(invitation);
            sender.SentInvitations.Add(invitation);

            await _invitationRepository.AddInvitation(invitation);

            await _userRepository.UpdateUser(sender);
            await _userRepository.UpdateUser(recipient);

            var addedInvitationResponse = invitation.ToInvitationResponseDto();
            return addedInvitationResponse;
        }

    }
}
