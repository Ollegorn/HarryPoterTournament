using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.TournamentStatsInterfaces
{
    public interface ITournamentStatsGetterService
    {
        Task<List<TournamentStats>> GetAllTournamentStats();

        Task<TournamentStats> GetTournamentStatsById(Guid id);

    }
}
