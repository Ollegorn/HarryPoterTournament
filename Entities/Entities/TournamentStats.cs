
namespace Entities.Entities
{
    public class TournamentStats
    {
        public Guid Id { get; set; }
        public int Wins { get; set; }

        public int Defeats { get; set; }

        public int TotalPoints { get; set; }

        public string UserId { get; set; }

        public Guid TournamentId { get; set; }


    }
}
