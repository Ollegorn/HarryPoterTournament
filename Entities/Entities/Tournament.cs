

namespace Entities.Entities
{
    public class Tournament
    {
        public string TournamentName { get; set; }

        public Guid TournamentId { get; set; }

        public List<string> Rules { get; set; }

        public string Description { get; set; }

        public string Prize { get; set; }

        public bool IsFlagged { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int ImageNumber { get; set; }
        public bool BalancedMode { get; set; }
        public bool EchoBan { get; set; }
        public bool CardBan { get; set; }
        public bool TwoWinsInThreeGames { get; set; }
        public bool Rewards {  get; set; }


        public ICollection<UserTournament> UserTournaments { get; set; }

        public List<Duel> TournamentDuels { get; set; }


    }
}
