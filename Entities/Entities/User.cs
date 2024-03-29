﻿using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class User : IdentityUser
    {
        public int ImageNumber { get; set; }
        public ICollection<TournamentStats> TournamentStats { get; set; }
        public ICollection<UserTournament> UserTournaments { get; set; }
        public ICollection<Invitation> SentInvitations { get; set; }
        public ICollection<Invitation> ReceivedInvitations { get; set; }
        public User()
        {
            TournamentStats = new List<TournamentStats>();
            SentInvitations = new List<Invitation>();
            ReceivedInvitations = new List<Invitation>();
        }

    }
}