using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class User : IdentityUser
    {
        public ICollection<TournamentStats> TournamentStats { get; set; }

        public User()
        {
            TournamentStats = new List<TournamentStats>();
        }

        public ICollection<UserTournament> UserTournaments { get; set; }
    }
}