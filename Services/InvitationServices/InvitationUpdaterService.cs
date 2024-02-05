using RepositoryContracts;
using ServiceContracts.Interfaces.InvitationInterfaces;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.InvitationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InvitationServices
{
    public class InvitationUpdaterService : IInvitationUpdaterService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IInvitationDeleterService _invitationDeleterService;
        private readonly IInvitationAdderService _invitationAdderService;

        public InvitationUpdaterService(IInvitationRepository invitationRepository, IInvitationDeleterService invitationDeleterService, IInvitationAdderService invitationAdderService)
        {
            _invitationRepository = invitationRepository;
            _invitationDeleterService = invitationDeleterService;
            _invitationAdderService = invitationAdderService;
        }

        public async Task<bool> ReturnInvitationToSender(InvitationUpdateRequestDto invitationUpdateRequestDto)
        {
            var existingInvitation = await _invitationRepository.GetInvitationById(invitationUpdateRequestDto.Id);

            if (existingInvitation == null)
                return false;

            var updatedSender = existingInvitation.Recipient.UserName;
            var updatedRecipient = existingInvitation.Sender.UserName;

            var invitationAddRequestDto = new InvitationAddRequestDto
            {
                SenderUsername = updatedSender,
                RecipientUsername = updatedRecipient,
                TournamentId = existingInvitation.TournamentId,
                DateTime = invitationUpdateRequestDto.DateTime,
                Message = invitationUpdateRequestDto.Message,
                IsAccepted = invitationUpdateRequestDto.IsAccepted,
                IsDeclined = invitationUpdateRequestDto.IsDeclined,
            };

            await _invitationAdderService.AddInvitation(invitationAddRequestDto);
            await _invitationDeleterService.DeleteInvitationById(existingInvitation.Id);

            

            return true;

        }
    }
}
