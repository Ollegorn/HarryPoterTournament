using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces.TournamentStatsInterfaces;

namespace HarryPotterTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentStatsController : ControllerBase
    {
        private readonly ITournamentStatsGetterService _getterService;
        private readonly ITournamentStatsDeleterService _deleterService;

        public TournamentStatsController(ITournamentStatsGetterService getterService, ITournamentStatsDeleterService deleterService)
        {
            _getterService = getterService;
            _deleterService = deleterService;
        }

        [HttpGet("AllTournamentStats")]
        public async Task<ActionResult<List<TournamentStats>>> GetAllTournamentStats()
        {
            var tournStats = await _getterService.GetAllTournamentStats();
            return Ok(tournStats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentStats>> GetTournamentStatsById(Guid tournStatsId)
        {
            var tournStats = await _getterService.GetTournamentStatsById(tournStatsId);
            if (tournStats == null)
            {
                return NotFound();
            }
            return Ok(tournStats);
        }

        [HttpDelete("DeleteTournamentStats")]
        public async Task<ActionResult> DeleteTournamentStats(Guid tournStatsId)
        {
            var isDeleted = await _deleterService.DeleteTournamentStatsById(tournStatsId);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok("Deleted Successfully");
        }
    }
}
