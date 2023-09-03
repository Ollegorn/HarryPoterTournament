using Entities.Entities;
using Microsoft.IdentityModel.Tokens;
using RepositoryContracts;
using ServiceContracts.Interfaces.TournamentInterfaces;
using ServiceContracts.TournamentDto;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TournamentServices
{
    public class TournamentUpdaterService : ITournamentUpdaterService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IDuelRepository _duelRepository;
        private readonly IUserRepository _userRepository;

        public TournamentUpdaterService(ITournamentRepository tournamentRepository,IDuelRepository duelRepository,IUserRepository userRepository)
        {
            _tournamentRepository = tournamentRepository;
            _duelRepository = duelRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddUserToTournament(Guid tournamnetId, string username)
        {
            return await _tournamentRepository.AddUserToTournament(tournamnetId, username);
        }

        //public async Task<bool> RemoveUserFromTournament(Guid tournamentId, string username)
        //{
        //    var tournament = await _tournamentRepository.GetTournamentById(tournamentId);
        //    if (tournament == null)
        //    {
        //        return false;
        //    }

        //    var user = await _userRepository.GetUserByUsername(username);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    var userToRemove = tournament.RegisteredUsers.FirstOrDefault(u => u.UserName == username);
        //    if (userToRemove == null)
        //    {
        //        return false;
        //    }

        //    tournament.RegisteredUsers.Remove(userToRemove);
        //    var tourn = tournament.ToTournament();
        //    await _tournamentRepository.UpdateTournament(tourn);
        //    return true;
        //}

        public async Task<bool> StartTournament(Guid tournamentId)
        {
            var registeredUsers = await _tournamentRepository.GetRegisteredUsersForTournament(tournamentId);

            if (registeredUsers == null || registeredUsers.Count < 2)
            {
                return false; 
            }

            for (int i = 0; i < registeredUsers.Count; i++)
            {
                for (int j = i + 1; j < registeredUsers.Count; j++)
                {
                    var duel = new Duel
                    {
                        DuelName = $"{registeredUsers[i].UserName} vs {registeredUsers[j].UserName}",
                        UserOne = registeredUsers[i],
                        UserTwo = registeredUsers[j],
                        TournamentId = tournamentId
                    };

                    await  _duelRepository.AddDuel(duel);
                }
            }

            return true; 
        }

        public async Task<bool> UpdateTournament(TournamentUpdateRequestDto tournamentUpdateRequestDto)
        {
            var existingTournament = await _tournamentRepository.GetTournamentById(tournamentUpdateRequestDto.TournamentId);
            if (existingTournament == null)
            {
                return false;
            }

            existingTournament.TournamentName = tournamentUpdateRequestDto.TournamentName;
            existingTournament.Prize = tournamentUpdateRequestDto.Prize;
            existingTournament.Rules = tournamentUpdateRequestDto.Rules;
            var tournament = existingTournament.ToTournament();

            await _tournamentRepository.UpdateTournament(tournament);

            return true;
        }
    }
}
