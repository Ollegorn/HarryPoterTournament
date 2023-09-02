using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public class UserUpdaterService : IUserUpdaterService
    {
        private readonly IUserRepository _userRepository;

        public UserUpdaterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> UpdateUserPoints(UserUpdateRequestDto userUpdateRequestDto)
        {
            var existingUser = await _userRepository.GetUserByUsername(userUpdateRequestDto.UserName);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Wins = userUpdateRequestDto.Wins;
            existingUser.Defeats = userUpdateRequestDto.Defeats;
            //existingUser.TotalTournamentPoints = 2 points for win 1 for defeat
            await _userRepository.UpdateUserPoints(existingUser);

            return true;
        }

        public async Task<bool> UpdateUserPointsAfterDuel(User user)
        {
            await _userRepository.UpdateUserPoints(user);
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
