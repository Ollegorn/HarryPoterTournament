
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public int CurrentTournamentPoints { get; set; }
    }
}