using Entities.Entities;
using ServiceContracts.DuelDto;

namespace RepositoryContracts
{
    public interface IDuelRepository
    {
        Task<List<DuelResponseDto>> GetAllDuels();

        Task<DuelResponseDto> GetDuelById(Guid id);

        Task<Duel> AddDuel(Duel duel);

        Task<bool> UpdateDuel(DuelUpdateRequestDto duelUpdateRequestDto);
        Task<bool> DeleteDuelById(Guid id);
    }
}