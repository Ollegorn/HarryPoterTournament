using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface ITournamentStatsRepository
    {
        Task<List<TournamentStats>> GetAllTournamentStats();

        Task<TournamentStats> GetTournamentStatsById(Guid id);

        Task<TournamentStats> AddTournamentStats(TournamentStats tournamentStats);

        Task<bool> UpdateTournamentStats(TournamentStats tournamentStats);

        Task<bool> DeleteTournamentStatsById(Guid id);

    }
}
