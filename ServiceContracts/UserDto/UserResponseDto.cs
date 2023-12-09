using Entities.Entities;
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
        public string Id { get; set; }
        public ICollection<TournamentStats> TournamentStats { get; set; }

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
                Id = user.Id,
                TournamentStats = user.TournamentStats.ToList()
            };
        }
    }
}
