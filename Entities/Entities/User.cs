using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class User : IdentityUser
    {
        public int CurrentTournamentPoints { get; set; }
    }
}