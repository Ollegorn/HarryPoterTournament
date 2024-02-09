using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.InvitationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvitationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invitation> AddInvitation(Invitation invitation)
        {
            _dbContext.Add(invitation);
            await _dbContext.SaveChangesAsync();
            return invitation;
        }

        public async Task<bool> DeleteInvitationById(Guid id)
        {
            var invitationToDelete = await _dbContext.Invitations.FindAsync(id);
            _dbContext.Invitations.Remove(invitationToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<InvitationResponseDto>> GetAllInvitations()
        {
            var invitations = await _dbContext.Invitations
                .Include(inv => inv.Recipient)
                .Include(inv => inv.Sender)
                .ToListAsync();

            var invitationsResponseDto = invitations.Select(inv => inv.ToInvitationResponseDto()).ToList();
            return invitationsResponseDto;
        }

        public async Task<InvitationResponseDto> GetInvitationById(Guid id)
        {
            var invitation = await _dbContext.Invitations
                .Include(inv => inv.Recipient)
                .Include(inv => inv.Sender)
                .FirstOrDefaultAsync();

            if (invitation == null)
                return null;

            var invitationResponseDto = invitation.ToInvitationResponseDto();

            return invitationResponseDto;
        }

        public async Task<List<InvitationResponseDto>> GetInvitationsByDuelId(Guid duelId)
        {
            var invitations = await _dbContext.Invitations
                .Include(inv => inv.Recipient)
                .Include(inv => inv.Sender)
                .Where(inv => inv.DuelId == duelId)
                .ToListAsync();

            var invitationsResponseDto = invitations.Select(inv => inv.ToInvitationResponseDto()).ToList();
            return invitationsResponseDto;

        }

        public async Task<bool> UpdateInvitation(InvitationUpdateRequestDto invitationUpdateRequestDto)
        {
            var invitationToUpdate = await _dbContext.Invitations.FindAsync(invitationUpdateRequestDto.Id);
            if (invitationToUpdate == null)
                return false;

            invitationToUpdate.IsAccepted = invitationUpdateRequestDto.IsAccepted;
            _dbContext.Invitations.Update(invitationToUpdate);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
