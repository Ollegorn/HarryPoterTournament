﻿using Entities.Entities;
using ServiceContracts.DuelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.DuelInterfaces
{
    public interface IDuelGetterService
    {
        Task<List<DuelResponseDto>> GetAllDuels();

        Task<DuelResponseDto> GetDuelById(Guid id);
    }
}
