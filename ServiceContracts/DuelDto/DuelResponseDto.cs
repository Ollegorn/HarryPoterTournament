using Entities.Entities;
using ServiceContracts.UserDto;

namespace ServiceContracts.DuelDto
{
    public class DuelResponseDto
    {
        public Guid DuelId { get; set; }

        public string DuelName { get; set; }

        public UserResponseDto UserOne { get; set; }

        public UserResponseDto UserTwo { get; set; }

        public int DuelWins { get; set; }

        public int DuelDefeats { get; set; }

        public bool isCompleted { get; set; }
        public bool isChallenged { get; set; }


    }
    public static class DuelExtentions
    {
        public static DuelResponseDto ToDuelResponseDto(this Duel duel)
        {
            var dto = new DuelResponseDto
            {
                DuelId = duel.DuelId,
                DuelName = duel.DuelName,
                DuelWins = duel.DuelWins,
                DuelDefeats = duel.DuelDefeats,
                isCompleted = duel.isCompleted,
                isChallenged = duel.isChallenged,

            };

            if (duel.UserOne != null)
            {
                dto.UserOne = new UserResponseDto { UserName = duel.UserOne.UserName, Id = duel.UserOne.Id,  TournamentStats = duel.UserOne.TournamentStats};
            }

            if (duel.UserTwo != null)
            {
                dto.UserTwo = new UserResponseDto { UserName = duel.UserTwo.UserName, Id = duel.UserTwo.Id, TournamentStats = duel.UserTwo.TournamentStats };
            }

            return dto;
        }

        public static List<DuelResponseDto> ToDuelResponseDtoList(this List<Duel> duels)
        {
            var duelResponse = new List<DuelResponseDto>();
            foreach (var duel in  duels)
            {
                duelResponse.Add(duel.ToDuelResponseDto());
            }
            return duelResponse;
        }
    }
}
