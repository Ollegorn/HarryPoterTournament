using Entities.Entities;
using ServiceContracts.DuelDto;
using ServiceContracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.UserInterfaces
{
    public interface IUserUpdaterService
    {
        Task<bool> UpdateUserPoints(UserUpdateRequestDto userUpdateRequestDto);

        Task<bool> UpdateUserPointsAfterDuel(User user);
        //Task<bool> UpdateUserPointsByDuel(DuelUpdateRequestDto duelUpdateRequest);
    }
}
