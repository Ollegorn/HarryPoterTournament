using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DuelDto;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.UserInterfaces;

namespace HarryPotterTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelController : ControllerBase
    {
        private readonly IDuelGetterService _getterService;
        private readonly IDuelAdderService _adderService;
        private readonly IDuelDeleterService _deleterService;
        private readonly IDuelUpdaterService _updaterService;


        public DuelController(IDuelGetterService getterService, IDuelUpdaterService updaterService, IDuelAdderService adderService, IDuelDeleterService deleterService)
        {
            _getterService = getterService;
            _updaterService = updaterService;
            _adderService = adderService;
            _deleterService = deleterService;
        }

        [HttpGet("AllDuels")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<List<DuelResponseDto>>> GetAllDuels()
        {
            var duels = await _getterService.GetAllDuels();
            return Ok(duels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuelResponseDto>> GetDuelById(Guid id)
        {
            var duel = await _getterService.GetDuelById(id);
            if (duel == null)
            {
                return NotFound();
            }
            return Ok(duel);
        }

        //[HttpPost("AddDuel")]
        //public async Task<ActionResult<DuelResponseDto>> AddDuel(DuelAddRequestDto duelAddRequest)
        //{
        //    var addedDuel = await _adderService.AddDuel(duelAddRequest);

        //    return Ok(addedDuel);
        //}

        [HttpDelete("DeleteDuel")]
        public async Task<ActionResult> DeleteDuel(Guid id)
        {
            var isDeleted = await _deleterService.DeleteDuel(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok("Deleted Successfully");
        }

        //[HttpPut("UpdateDuel")]
        //public async Task<ActionResult> UpdateDuel(DuelUpdateRequestDto duelUpdateRequest)
        //{
        //    var updatedDuel = await _updaterService.UpdateDuelPoints(duelUpdateRequest);

        //    if (!updatedDuel)
        //    {
        //        return NotFound("Duel Not Found");
        //    }

        //    return Ok("Updated Successfully");
        //}
    }
}
