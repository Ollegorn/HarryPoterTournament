using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.UserDto
{
    public class UserUpdateRequestDto
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public TournamentStats TournamentStats { get; set; }


    }

}
