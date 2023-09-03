using Entities.Entities;
using ServiceContracts.TournamentDto;




namespace ServiceContracts.Interfaces.TournamentInterfaces
{
    public interface ITournamentAdderService
    {
        Task<Tournament> AddTournament(TournamentAddRequestDto tournamentAddRequestDto);
    }
}
