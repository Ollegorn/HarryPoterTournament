using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class DuelRepository : IDuelRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DuelRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Duel> AddDuel(Duel duel)
        {
            _dbContext.Add(duel);
            await _dbContext.SaveChangesAsync();
            return duel;
        }

        public async Task DeleteDuelById(Guid id)
        {
            var duelToDelete = await _dbContext.Duels.FindAsync(id);
            _dbContext.Duels.Remove(duelToDelete);
            await _dbContext.SaveChangesAsync();
            return;
        }

        public async Task<List<Duel>> GetAllDuels()
        {
            return await _dbContext.Duels.ToListAsync();
        }

        public async Task<Duel> GetDuelById(Guid id)
        {
            var duel = await _dbContext.Duels.FindAsync(id);
            return duel;
        }

        public async Task UpdateDuel(Duel duel)
        {
            var duelToUpdate = await _dbContext.Duels.FindAsync(duel.DuelId);
            if (duelToUpdate == null) 
            {
                return;
            }
            duelToUpdate.MatchName = duel.MatchName;
            duelToUpdate.Users = duel.Users;
            await _dbContext.SaveChangesAsync();

            return;
        }
    }
}