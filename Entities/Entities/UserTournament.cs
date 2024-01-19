namespace Entities.Entities
{
    public class UserTournament
    {
        public string UserId { get; set; }
        public Guid TournamentId { get; set; }

        public User User { get; set; }
        public Tournament Tournament { get; set; }
    }

}
