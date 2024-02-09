using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.InvitationInterfaces;
using ServiceContracts.InvitationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InvitationServices
{
    public class InvitationDeleterService : IInvitationDeleterService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IDuelGetterService _duelGetterService;
        private readonly IDuelUpdaterService _duelUpdaterService;

        public InvitationDeleterService(IInvitationRepository invitationRepository, IDuelGetterService duelGetterService, IDuelUpdaterService duelUpdaterService)
        {
            _invitationRepository = invitationRepository;
            _duelGetterService = duelGetterService;
            _duelUpdaterService = duelUpdaterService;
        }

        public async Task<bool> DeleteInvitationById(Guid id)
        {
            var invitationToDelete = await _invitationRepository.GetInvitationById(id);
            var duelResponseDto = await _duelGetterService.GetDuelById(invitationToDelete.DuelId);

            var duelUpdate = new DuelUpdateRequestDto
            {
                DuelId = duelResponseDto.DuelId,
                isChallenged = false,
            };

            await _duelUpdaterService.UpdateDuel(duelUpdate);
            await _invitationRepository.DeleteInvitationById(id);

            return true;
        }

        public async Task<bool> DeleteInvitationByTournamentId(Guid tournamentId)
        {
            var invitations = await _invitationRepository.GetAllInvitations();
            foreach (var invitationToDelete in invitations)
            {
                if(invitationToDelete.TournamentId == tournamentId)
                {
                    await _invitationRepository.DeleteInvitationById(invitationToDelete.Id);
                }
            }
            return true;
        }

        public async Task<bool> DeleteInvitationsByDuelId(Guid duelId)
        {
            var invitations = await _invitationRepository.GetInvitationsByDuelId(duelId);
            foreach(var invitationToDelete in invitations)
            {
                await _invitationRepository.DeleteInvitationById(invitationToDelete.Id);
            }
            return true;
        }
    }
}
