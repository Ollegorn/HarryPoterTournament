using Entities.Entities;
using ServiceContracts.DuelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.DuelInterfaces
{
    public interface IDuelUpdaterService
    {
        Task<bool> UpdateDuelPoints(DuelUpdateRequestDto duelUpdateRequest);

        Task<bool> UpdateDuel(DuelUpdateRequestDto duelUpdateRequestDto);
    }
}
