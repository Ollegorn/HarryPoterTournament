using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;
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
        private readonly IDuelUpdaterService _duelUpdaterService;

        public InvitationUpdaterService(IInvitationRepository invitationRepository, IInvitationDeleterService invitationDeleterService, IInvitationAdderService invitationAdderService, IDuelUpdaterService duelUpdaterService)
        {
            _invitationRepository = invitationRepository;
            _invitationDeleterService = invitationDeleterService;
            _invitationAdderService = invitationAdderService;
            _duelUpdaterService = duelUpdaterService;
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
                DuelId = existingInvitation.DuelId,
                DateTime = invitationUpdateRequestDto.DateTime,
                Message = invitationUpdateRequestDto.Message,
                IsAccepted = invitationUpdateRequestDto.IsAccepted,
                IsDeclined = invitationUpdateRequestDto.IsDeclined,
            };

            await _invitationDeleterService.DeleteInvitationById(existingInvitation.Id);
            await _invitationAdderService.AddInvitation(invitationAddRequestDto);

            

            return true;

        }

        public async Task<bool> AcceptInvitation(InvitationUpdateRequestDto invitationUpdateRequestDto)
        {
            var existingInvitation = await _invitationRepository.GetInvitationById(invitationUpdateRequestDto.Id);

            if (existingInvitation == null)
                return false;

            if (existingInvitation.IsAccepted == true && existingInvitation.IsDeclined == true)
            {
                var duelUpdateDto = new DuelUpdateRequestDto
                {
                    DuelId = existingInvitation.DuelId,
                    isChallenged = false,
                };

                await _duelUpdaterService.UpdateDuel(duelUpdateDto);
            }

            await _invitationRepository.UpdateInvitation(invitationUpdateRequestDto);
            return true;

        }
    }
}
