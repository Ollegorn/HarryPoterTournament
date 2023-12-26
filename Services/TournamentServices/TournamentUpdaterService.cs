using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.TournamentInterfaces;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.TournamentDto;
using ServiceContracts.UserDto;

namespace Services.TournamentServices
{
    public class TournamentUpdaterService : ITournamentUpdaterService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IDuelRepository _duelRepository;
        private readonly IUserGetterService _userGetterService;
        private readonly IUserUpdaterService _userUpdaterService;
        private readonly IUserRepository _userRepository;
        private readonly IDuelUpdaterService _duelUpdaterService;

        public TournamentUpdaterService(ITournamentRepository tournamentRepository,IDuelRepository duelRepository,IUserGetterService userGetterService,IUserUpdaterService userUpdaterService,IUserRepository userRepository, IDuelUpdaterService duelUpdaterService)
        {
            _tournamentRepository = tournamentRepository;
            _duelRepository = duelRepository;
            _userGetterService = userGetterService;
            _userUpdaterService = userUpdaterService;
            _userRepository = userRepository;
            _duelUpdaterService = duelUpdaterService;
        }

        public async Task<bool> AddUserToTournament(Guid tournamnetId, string username)
        {
            var tournament = await _tournamentRepository.GetTournamentById(tournamnetId);
            
            if (tournament == null)
            {
                return false;
            }
            var user = await _userGetterService.GetUserByUsername(username);
            var userResponse = UserExtentions.ToUserResponseDto(user);
            if (user == null)
            {
                return false;
            }

            if (tournament != null && user != null && tournament.RegisteredUsers != null)
            {
                if (tournament.RegisteredUsers.Contains(userResponse))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            tournament.RegisteredUsers.Add(userResponse);
            await _tournamentRepository.AddUserToTournament(tournament.TournamentId, userResponse.UserName);

            return true;
        }

        

        public async Task<bool> RemoveUserFromTournament(Guid tournamentId, string username)
        {
            var tournament = await _tournamentRepository.GetTournamentById(tournamentId);
            if (tournament == null)
            {
                return false;
            }

            var userToRemove = tournament.RegisteredUsers.FirstOrDefault(u => u.UserName == username);
            if (userToRemove == null)
            {
                return false;
            }

            tournament.RegisteredUsers.Remove(userToRemove);
            var tourn = tournament.ToTournament();
            await _tournamentRepository.UpdateTournament(tourn);

            foreach (var duel in tournament.TournamentDuels)
            {
                var duelUpdateDto = new DuelUpdateRequestDto
                {
                    DuelId = duel.DuelId
                };

                if (!duel.isCompleted)
                {
                    if (duel.UserOne?.UserName == username && duel.UserTwo != null)
                    {
                        duelUpdateDto.UserOneWins = 0;
                        duelUpdateDto.UserOneDefeats = 2;
                        duelUpdateDto.isCompleted = true;
                    }
                    else if (duel.UserOne != null && duel.UserTwo?.UserName == username)
                    {
                        duelUpdateDto.UserOneWins = 2;
                        duelUpdateDto.UserOneDefeats = 0;
                        duelUpdateDto.isCompleted = true;
                    }

                    await _duelUpdaterService.UpdateDuelPoints(duelUpdateDto);
                    await _duelUpdaterService.UpdateDuel(duelUpdateDto);
                }
            }

            return true;
        }


        public async Task<bool> StartTournament(Guid tournamentId)
        {
            var registeredUsers = await _tournamentRepository.GetRegisteredUsersForTournament(tournamentId);

            if (registeredUsers == null || registeredUsers.Count < 2)
            {
                return false;
            }

            foreach (var user in registeredUsers)
            {
                var tournamentStats = user.TournamentStats.FirstOrDefault(ts => ts.TournamentId == tournamentId);

                if (tournamentStats == null)
                {
                    tournamentStats = new TournamentStats
                    {
                        TournamentId = tournamentId,
                        Wins = 0,
                        Defeats = 0,
                        TotalPoints = 0
                    };

                    user.TournamentStats.Add(tournamentStats);
                }
                else
                {
                    tournamentStats.Wins = 0;
                    tournamentStats.Defeats = 0;
                    tournamentStats.TotalPoints = 0;
                }

                await _userRepository.UpdateUserPoints(tournamentId, user);
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

                    await _duelRepository.AddDuel(duel);
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
