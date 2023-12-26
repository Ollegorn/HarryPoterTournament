using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.InvitationDto
{
    public class InvitationAddRequestDto
    {
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public Guid TournamentId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }

        public Invitation ToInvitation(User sender, User recipient)
        {
            return new Invitation
            {
                Sender= sender,
                Recipient = recipient,
                RecipientUsername = RecipientUsername,
                SenderUsername = SenderUsername,
                TournamentId = TournamentId,
                DateTime = DateTime,
                Message = Message,
                IsAccepted = IsAccepted,
                IsDeclined = IsDeclined
            };
        }
    }
}
