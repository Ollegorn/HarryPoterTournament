
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class User : IdentityUser
    {
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public int TotalTournamentPoints { get; set; }

       
        public ICollection<UserTournament> UserTournaments { get; set; }
    }
}