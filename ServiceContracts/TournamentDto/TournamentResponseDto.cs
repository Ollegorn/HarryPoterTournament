using Entities.Entities;
using ServiceContracts.DuelDto;
using ServiceContracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.TournamentDto
{
    public class TournamentResponseDto
    {
        public string TournamentName { get; set; }

        public Guid TournamentId { get; set; }

        public string Rules { get; set; }

        public string Prize { get; set; }

        public List<UserResponseDto> RegisteredUsers { get; set; }

        public List<DuelResponseDto> TournamentDuels { get; set; }



        public Tournament ToTournament()
        {
            return new Tournament
            {
                TournamentId = TournamentId,
                TournamentName = TournamentName,
                Rules = Rules,
                Prize = Prize
            };
        }
    }
    public static class TournamentExtentions
    {
        public static TournamentResponseDto ToTournamentResponseDto(this Tournament tournament)
        {
            var tournamentDto = new TournamentResponseDto
            {
                TournamentName= tournament.TournamentName,
                TournamentId = tournament.TournamentId,
                Rules = tournament.Rules,
                Prize = tournament.Prize,
                RegisteredUsers = tournament.RegisteredUsers.Select(u => new UserResponseDto
                {
                    UserName = u.UserName,
                    Wins = u.Wins,
                    Defeats = u.Defeats,
                    TotalTournamentPoints = u.TotalTournamentPoints
                }).ToList(),
                TournamentDuels = tournament.TournamentDuels?.Select(d => new DuelResponseDto
                {
                    DuelId = d.DuelId,
                    DuelName = d.DuelName,
                    UserOne = d.UserOne != null ? new UserResponseDto
                    {
                        UserName = d.UserOne.UserName,
                        Wins = d.UserOne.Wins,
                        Defeats = d.UserOne.Defeats,
                        TotalTournamentPoints = d.UserOne.TotalTournamentPoints
                    } : null, // Handle the case where UserOne is null
                    UserTwo = d.UserTwo != null ? new UserResponseDto
                    {
                        UserName = d.UserTwo.UserName,
                        Wins = d.UserTwo.Wins,
                        Defeats = d.UserTwo.Defeats,
                        TotalTournamentPoints = d.UserTwo.TotalTournamentPoints
                    } : null, // Handle the case where UserTwo is null
                }).ToList()
        };

            return tournamentDto;
        }

        public static List<DuelResponseDto> ToDuelResponseDtoList(this List<Duel> duels)
        {
            var duelResponse = new List<DuelResponseDto>();
            foreach (var duel in duels)
            {
                duelResponse.Add(duel.ToDuelResponseDto());
            }
            return duelResponse;
        }
    }

}
