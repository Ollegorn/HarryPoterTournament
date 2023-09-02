using Entities.Migrations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class User : IdentityUser
    {
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public int TotalTournamentPoints { get; set; }

    }
}