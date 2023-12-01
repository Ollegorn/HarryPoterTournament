

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

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ImageNumber { get; set; }


        public ICollection<UserTournament> UserTournaments { get; set; }

        public List<Duel> TournamentDuels { get; set; }


    }
}
