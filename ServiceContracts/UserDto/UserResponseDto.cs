using Entities.Entities;
using ServiceContracts.InvitationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.UserDto
{
    public class UserResponseDto
    {
        public string UserName { get; set; }
        public int ImageNumber { get; set; }
        public string Id { get; set; }
        public ICollection<TournamentStats> TournamentStats { get; set; }

        public ICollection<InvitationResponseDto> SentInvitations { get; set; }
        public ICollection<InvitationResponseDto> ReceivedInvitations { get; set; }

        public User ToUser()
        {
            return new User
            {
                ImageNumber = ImageNumber,
                UserName = UserName,
                Id = Id,
                TournamentStats = TournamentStats,

            };
        }
    }

    public static class UserExtentions
    {
        public static UserResponseDto ToUserResponseDto(this User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                UserName = user.UserName,
                ImageNumber = user.ImageNumber,
                Id = user.Id,
                TournamentStats = user.TournamentStats.ToList(),
                SentInvitations = user.SentInvitations.ToInvitationResponseDtoList(),
                ReceivedInvitations = user.ReceivedInvitations.ToInvitationResponseDtoList()

            };
        }
        public static List<InvitationResponseDto> ToInvitationResponseDtoList(this ICollection<Invitation> invitations)
        {
            return invitations.Select(invitation => invitation.ToInvitationResponseDto()).ToList();
        }
    }
}
