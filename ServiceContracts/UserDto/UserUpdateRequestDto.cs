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

        public int Wins { get; set; }

        public int Defeats { get; set; }



    }

}
