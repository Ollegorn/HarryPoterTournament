using RepositoryContracts;
using ServiceContracts.Interfaces.DuelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DuelServices
{
    public class DuelDeleterService : IDuelDeleterService
    {
        private readonly IDuelRepository _duelRepository;

        public DuelDeleterService(IDuelRepository duelRepository)
        {
            _duelRepository = duelRepository;
        }

        public async Task<bool> DeleteDuel(Guid id)
        {
            var duelToDelete = await _duelRepository.DeleteDuelById(id);

            if (!duelToDelete)
            {
                return false;
            }
            return true;

        }
    }
}
