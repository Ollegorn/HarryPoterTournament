using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.UserInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DuelServices
{
    public class DuelUpdaterService : IDuelUpdaterService
    {
        private readonly IDuelRepository _duelRepository;
        private readonly IUserUpdaterService _userUpdaterService;
        private readonly IUserGetterService _userGetterService;

        public DuelUpdaterService(IDuelRepository duelRepository,IUserUpdaterService userUpdaterService,IUserGetterService userGetterService)
        {
            _duelRepository = duelRepository;
            _userUpdaterService = userUpdaterService;
            _userGetterService = userGetterService;
        }

        public async Task<bool> UpdateDuel(DuelUpdateRequestDto duelUpdateRequestDto)
        {
            await _duelRepository.UpdateDuel(duelUpdateRequestDto);
            return true;
        }

        public async Task<bool> UpdateDuelPoints(DuelUpdateRequestDto duelUpdateRequest)
        {
            var existingDuel = await _duelRepository.GetDuelById(duelUpdateRequest.DuelId);
            if (existingDuel == null)
            {
                return false;
            }
            
            if (existingDuel.isCompleted)
            {
                return false;
            }
            var userOneWins = duelUpdateRequest.UserOneWins;
            var userOneDefeats = duelUpdateRequest.UserOneDefeats;


            User userOne = await  _userGetterService.GetUserByUsername(existingDuel.UserOne.UserName);
            var userTwo =await  _userGetterService.GetUserByUsername(existingDuel.UserTwo.UserName);

            userOne.Wins += userOneWins;
            userOne.Defeats += userOneDefeats;
            userTwo.Wins += userOneDefeats;
            userTwo.Defeats += userOneWins;

            await _userUpdaterService.UpdateUserPointsAfterDuel(userOne);
            await _userUpdaterService.UpdateUserPointsAfterDuel(userTwo);
           

            return true;



        }
    }
}
