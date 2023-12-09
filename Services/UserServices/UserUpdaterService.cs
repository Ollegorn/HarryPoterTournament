using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.UserDto;

namespace Services.UserServices
{
    public class UserUpdaterService : IUserUpdaterService
    {
        private readonly IUserRepository _userRepository;

        public UserUpdaterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> UpdateUserPoints(Guid tournamentId, UserUpdateRequestDto userUpdateRequestDto)
        {
            var existingUser = await _userRepository.GetUserByUsername(userUpdateRequestDto.UserName);
            if (existingUser == null)
            {
                return false;
            }

            var tournamentStats = existingUser.TournamentStats
                .FirstOrDefault(ts => ts.TournamentId == tournamentId);

           
            tournamentStats.Wins = userUpdateRequestDto.TournamentStats?.Wins ?? 0;
            tournamentStats.Defeats = userUpdateRequestDto.TournamentStats?.Defeats ?? 0;

            await _userRepository.UpdateUserPoints(tournamentId, existingUser);

            return true;
        }



        public async Task<bool> UpdateUserPointsAfterDuel(Guid tournamentId, User user)
        {
            await _userRepository.UpdateUserPoints(tournamentId,user);
            return true;
        }

        //public async Task<bool> UpdateUserPointsByDuel(DuelUpdateRequestDto duelUpdateRequest)
        //{
        //    var existingUserOne = await _userRepository.GetUserByUsername(duelUpdateRequest.UserOneUsername);
        //    var existingUserTwo = await _userRepository.GetUserByUsername(duelUpdateRequest.UserTwoUsername);

        //    existingUserOne.CurrentTournamentPoints += duelUpdateRequest.UserOnePoints;
        //    existingUserTwo.CurrentTournamentPoints += duelUpdateRequest.UserTwoPoints;

        //    await _userRepository.UpdateUserPoints(existingUserOne);
        //    await _userRepository.UpdateUserPoints(existingUserTwo);

        //    return true;
        //}
    }
}
