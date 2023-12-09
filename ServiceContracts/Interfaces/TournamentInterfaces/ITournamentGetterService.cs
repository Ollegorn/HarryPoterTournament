
using ServiceContracts.TournamentDto;

namespace ServiceContracts.Interfaces.TournamentInterfaces
{
    public interface ITournamentGetterService
    {
        Task<List<TournamentResponseDto>> GetAllTournaments();

        Task<TournamentResponseDto> GetTournamentById(Guid id);

        Task<TournamentResponseDto> GetTournamentByDuelId(Guid duelId);
    }
}
