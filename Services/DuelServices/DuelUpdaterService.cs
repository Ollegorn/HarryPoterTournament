using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.TournamentInterfaces;
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
        private readonly ITournamentGetterService _tournamentGetterService;

        public DuelUpdaterService(IDuelRepository duelRepository, IUserUpdaterService userUpdaterService, IUserGetterService userGetterService, ITournamentGetterService tournamentGetterService)
        {
            _duelRepository = duelRepository;
            _userUpdaterService = userUpdaterService;
            _userGetterService = userGetterService;
            _tournamentGetterService = tournamentGetterService;
        }

        public async Task<bool> UpdateDuel(DuelUpdateRequestDto duelUpdateRequestDto)
        {
            await _duelRepository.UpdateDuel(duelUpdateRequestDto);
            return true;
        }

        public async Task<bool> UpdateDuelPoints(DuelUpdateRequestDto duelUpdateRequest)
        {
            var existingDuel = await _duelRepository.GetDuelById(duelUpdateRequest.DuelId);
            var tournamentId = (await _tournamentGetterService.GetTournamentByDuelId(duelUpdateRequest.DuelId)).TournamentId;
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


            var userOne = await _userGetterService.GetUserByUsername(existingDuel.UserOne.UserName);
            var userTwo = await _userGetterService.GetUserByUsername(existingDuel.UserTwo.UserName);

            var userOneTournamentStats = userOne.TournamentStats.FirstOrDefault(ts => ts.TournamentId == tournamentId);
            var userTwoTournamentStats = userTwo.TournamentStats.FirstOrDefault(ts => ts.TournamentId == tournamentId);

            if (userOneWins > userOneDefeats)
            {
                userOneTournamentStats.Wins += 1;
                userTwoTournamentStats.Defeats += 1;
                userOneTournamentStats.TotalPoints += 2;
                if(userOneDefeats == 1)
                {
                    userTwoTournamentStats.TotalPoints += 1;
                }
            }
            if (userOneWins < userOneDefeats)
            {
                userOneTournamentStats.Defeats += 1;
                userTwoTournamentStats.Wins += 1;
                userTwoTournamentStats.TotalPoints += 2;
                if (userOneWins == 1)
                {
                    userOneTournamentStats.TotalPoints += 1;
                }
            }


            await _userUpdaterService.UpdateUserPointsAfterDuel(tournamentId, userOne);
            await _userUpdaterService.UpdateUserPointsAfterDuel(tournamentId, userTwo);
           

            return true;

        }
    }
}
