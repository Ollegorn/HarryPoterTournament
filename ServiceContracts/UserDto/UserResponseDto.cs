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

        public int Wins { get; set; }

        public int Defeats { get; set; }

        public int TotalTournamentPoints { get; set; }

        public string Id { get; set; }
    }

    public static class UserExtentions
    {
        public static UserResponseDto ToUserResponseDto(this User user)
        {
            return new UserResponseDto
            {
                UserName = user.UserName,
                Wins = user.Wins,
                Defeats = user.Defeats,
                TotalTournamentPoints = user.TotalTournamentPoints,
                Id = user.Id
                
            };
        }
    }
}
