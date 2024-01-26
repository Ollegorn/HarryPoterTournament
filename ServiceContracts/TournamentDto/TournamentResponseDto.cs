using Entities.Entities;
using ServiceContracts.DuelDto;
using ServiceContracts.UserDto;

namespace ServiceContracts.TournamentDto
{
    public class TournamentResponseDto
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
        public bool Rewards { get; set; }

        public List<UserResponseDto> RegisteredUsers { get; set; }

        public List<DuelResponseDto> TournamentDuels { get; set; }




        public Tournament ToTournament()
        {
            return new Tournament
            {
                TournamentId = TournamentId,
                TournamentName = TournamentName,
                Rules = Rules,
                Prize = Prize,
                ImageNumber = ImageNumber,
                Description = Description,
                IsFlagged = IsFlagged,
                StartDate = StartDate,
                EndDate = EndDate,
                BalancedMode = BalancedMode,
                EchoBan = EchoBan,
                CardBan = CardBan,
                TwoWinsInThreeGames = TwoWinsInThreeGames,
                Rewards = Rewards,
                UserTournaments = RegisteredUsers?.Select(user => new UserTournament
                {
                    UserId = user.Id,
                    TournamentId = TournamentId
                }).ToList()
            };
        }
    }
    public static class TournamentExtensions
    {
        public static TournamentResponseDto ToTournamentResponseDto(this Tournament tournament)
        {
            var tournamentDto = new TournamentResponseDto
            {
                TournamentName = tournament.TournamentName,
                TournamentId = tournament.TournamentId,
                Rules = tournament.Rules,
                Prize = tournament.Prize,
                RegisteredUsers = tournament.UserTournaments?.Select(ut => ut.User.ToUserResponseDto()).ToList(),
                TournamentDuels = tournament.TournamentDuels?.Select(d => d.ToDuelResponseDto()).ToList(),
                Description = tournament.Description,
                IsFlagged= tournament.IsFlagged,
                StartDate= tournament.StartDate,
                EndDate= tournament.EndDate,
                ImageNumber= tournament.ImageNumber,
                BalancedMode= tournament.BalancedMode,
                EchoBan= tournament.EchoBan,
                CardBan= tournament.CardBan,
                TwoWinsInThreeGames= tournament.TwoWinsInThreeGames,
                Rewards= tournament.Rewards,

            };

            return tournamentDto;
        }


        public static UserResponseDto ToUserResponseDto(this User user)
        {
            if (user == null)
            {
                return null;
            }


            return new UserResponseDto
            {
                UserName = user.UserName,
                Id = user.Id,
                TournamentStats = user.TournamentStats
            };
        }


        public static DuelResponseDto ToDuelResponseDto(this Duel duel)
        {
            return new DuelResponseDto
            {
                DuelId = duel.DuelId,
                DuelName = duel.DuelName,
                UserOne = duel.UserOne?.ToUserResponseDto(),
                UserTwo = duel.UserTwo?.ToUserResponseDto(),
                DuelWins = duel.DuelWins,
                DuelDefeats = duel.DuelDefeats,
                isCompleted = duel.isCompleted
            };
        }

        public static List<DuelResponseDto> ToDuelResponseDtoList(this List<Duel> duels)
        {
            return duels?.Select(duel => duel.ToDuelResponseDto()).ToList();
        }
    }


}
