namespace Entities.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public Guid TournamentId { get; set; }
        public Guid DuelId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }

    }
}
