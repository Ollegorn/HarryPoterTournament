using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.TournamentDto
{
    public class AddUserToTournDto
    {
        public Guid TournamentId { get; set; }
        public string Username { get; set; }
    }
}
