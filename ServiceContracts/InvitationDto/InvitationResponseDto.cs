using Entities.Entities;
using ServiceContracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.InvitationDto
{
    public class InvitationResponseDto
    {
        public Guid Id { get; set; }
        public UserResponseDto Recipient { get; set; }
        public UserResponseDto Sender { get; set; }
        public Guid TournamentId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }

        public Invitation ToInvitation(User sender, User recipient)
        {
            return new Invitation
            {
                Id = Id,
                Sender = sender,
                Recipient = recipient,
                TournamentId = TournamentId,
                DateTime = DateTime,
                Message = Message,
                IsAccepted = IsAccepted,
                IsDeclined = IsDeclined
            };
        }
    }

    public static class InvitationExtentions
    {
        public static InvitationResponseDto ToInvitationResponseDto(this Invitation invitation)
        {
            var dto = new InvitationResponseDto
            {
                Id = invitation.Id,
                Recipient = invitation.Recipient.ToUserResponseDto(),
                Sender = invitation.Sender.ToUserResponseDto(),
                TournamentId = invitation.TournamentId,
                DateTime = invitation.DateTime,
                Message = invitation.Message,
                IsAccepted = invitation.IsAccepted,
                IsDeclined = invitation.IsDeclined,
            };
            return dto;
        }
        public static UserResponseDto ToUserResponseDto(this User user)
        {
            if (user == null)
            {
                return null;
            }


            return new UserResponseDto
            {
                UserName = user.UserName,
                Id = user.Id,
                ImageNumber = user.ImageNumber,
                TournamentStats = user.TournamentStats
            };
        }

        public static List<InvitationResponseDto> ToInvitationResponseDtoList(this List<Invitation> invitations)
        {
            var invitationsResponse = new List<InvitationResponseDto>();
            foreach (var invitation in invitations)
            {
                invitationsResponse.Add(invitation.ToInvitationResponseDto());
            }
            return invitationsResponse;
        }
    }
}
