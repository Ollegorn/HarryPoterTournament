using Entities.Entities;

namespace RepositoryContracts
{
    public interface IDuelRepository
    {
        Task<List<Duel>> GetAllDuels();

        Task<Duel> GetDuelById(Guid id);

        Task<Duel> AddDuel(Duel duel);

        Task UpdateDuel(Duel duel);
        Task DeleteDuelById(Guid id);
    }
}