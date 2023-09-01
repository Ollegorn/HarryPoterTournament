using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.DuelInterfaces
{
    public interface IDuelGetterService
    {
        Task<List<Duel>> GetAllDuels();

        Task<Duel> GetDuelById(Guid id);
    }
}
