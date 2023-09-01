using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces.DuelInterfaces;

namespace HarryPotterTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelController : ControllerBase
    {
        private readonly IDuelGetterService _getterService;

        public DuelController(IDuelGetterService getterService)
        {
            _getterService = getterService;
        }

        [HttpGet("AllDuels")]
        public async Task<ActionResult<List<Duel>>> GetAllDuels()
        {
            var duels = await _getterService.GetAllDuels();
            return Ok(duels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Duel>> GetDuelById(Guid id)
        {
            var duel = await _getterService.GetDuelById(id);
            if (duel == null)
            {
                return NotFound();
            }
            return Ok(duel);
        }

    }
}
