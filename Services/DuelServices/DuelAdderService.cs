using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.UserInterfaces;

namespace Services.DuelServices
{
    public class DuelAdderService : IDuelAdderService
    {
        private readonly IDuelRepository _duelRepository;
        private readonly IUserGetterService _userGetterService;

        public DuelAdderService(IDuelRepository duelRepository, IUserGetterService userGetterService)
        {
            _duelRepository = duelRepository;
            _userGetterService = userGetterService;
        }

        public async Task<DuelResponseDto> AddDuel(DuelAddRequestDto duelAddRequest)
        {
            User userOne = await _userGetterService.GetUserByUsername(duelAddRequest.UserOneUsername);
            User userTwo = await _userGetterService.GetUserByUsername(duelAddRequest.UserTwoUsername);

            var duel = duelAddRequest.ToDuel();
            duel.UserOne = userOne;
            duel.UserTwo = userTwo;

            var addedDuel =  await _duelRepository.AddDuel(duel);
            var addedDuelResponse = addedDuel.ToDuelResponseDto();


            return addedDuelResponse;
        }
    }
}
