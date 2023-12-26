using RepositoryContracts;
using ServiceContracts.Interfaces.InvitationInterfaces;
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

        public InvitationDeleterService(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task<bool> DeleteInvitationById(Guid id)
        {
            var invitationToDelete = await _invitationRepository.DeleteInvitationById(id);
            if (!invitationToDelete)
                return false;

            return true;
        }
    }
}
