using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;

namespace Services.DuelServices
{
    public class DuelGetterService : IDuelGetterService
    {
        private readonly IDuelRepository _duelRepository;

        public DuelGetterService(IDuelRepository duelRepository)
        {
            _duelRepository = duelRepository;
        }

        public async Task<List<DuelResponseDto>> GetAllDuels()
        {
            var duels = await _duelRepository.GetAllDuels();


            return duels;
        }

        public async Task<DuelResponseDto> GetDuelById(Guid id)
        {
            var duel = await _duelRepository.GetDuelById(id);

            return duel;
        }
    }
}
