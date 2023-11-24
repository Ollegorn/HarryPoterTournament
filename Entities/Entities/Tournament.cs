using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Tournament
    {
        public string TournamentName { get; set; }

        public Guid TournamentId { get; set; }

        public string Rules { get; set; }

        public string Prize { get; set; }

        public int ImageNumber { get; set; }

        public List<User> RegisteredUsers { get; set; }

        public List<Duel> TournamentDuels { get; set; }


    }
}
