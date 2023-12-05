using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DuelDto;

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

        public async Task<bool> DeleteDuelById(Guid id)
        {
            var duelToDelete = await _dbContext.Duels.FindAsync(id);
            _dbContext.Duels.Remove(duelToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<DuelResponseDto>> GetAllDuels()
        {
            var duels = await _dbContext.Duels
                .Include(duel => duel.UserOne)
                .Include(duel => duel.UserTwo)
                .ToListAsync();

            var duelsResponse = duels.Select(duel => duel.ToDuelResponseDto()).ToList();


            return duelsResponse;
        }

        public async Task<DuelResponseDto> GetDuelById(Guid id)
        {
            var duel = await _dbContext.Duels
                .Include(duel => duel.UserOne)
                .Include(duel => duel.UserTwo)
                .FirstOrDefaultAsync(duel => duel.DuelId == id);

            var duelResponse = duel.ToDuelResponseDto();
            return duelResponse;
        }

        public async Task<bool> UpdateDuel(DuelUpdateRequestDto duelUpdateRequestDto)
        {
            var duelToUpdate = await _dbContext.Duels.FindAsync(duelUpdateRequestDto.DuelId);
            if (duelToUpdate == null)
            {
                return false;
            }
            duelToUpdate.DuelWins = duelUpdateRequestDto.UserOneWins;
            duelToUpdate.DuelDefeats = duelUpdateRequestDto.UserOneDefeats;
            duelToUpdate.isCompleted = duelUpdateRequestDto.isCompleted;
            _dbContext.Duels.Update(duelToUpdate);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}