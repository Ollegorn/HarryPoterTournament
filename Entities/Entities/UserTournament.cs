using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class UserTournament
    {
        public string UserId { get; set; }
        public Guid TournamentId { get; set; }

        public User User { get; set; }
        public Tournament Tournament { get; set; }
    }

}
