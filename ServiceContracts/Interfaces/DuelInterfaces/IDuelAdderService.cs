using Entities.Entities;
using ServiceContracts.DuelDto;



namespace ServiceContracts.Interfaces.DuelInterfaces
{
    public interface IDuelAdderService
    {
        Task<DuelResponseDto> AddDuel(DuelAddRequestDto duelAddRequest);

    }
}
