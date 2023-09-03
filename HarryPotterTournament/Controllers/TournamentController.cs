using Azure.Core;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces.TournamentInterfaces;
using ServiceContracts.TournamentDto;

namespace HarryPotterTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentGetterService _getterService;
        private readonly ITournamentDeleterService _deleterService;
        private readonly ITournamentAdderService _adderService;
        private readonly ITournamentUpdaterService _updaterService;


        public TournamentController(ITournamentDeleterService tournamentDeleterService, ITournamentGetterService getterService, ITournamentAdderService tournamentAdderService, ITournamentUpdaterService tournamentUpdaterService)
        {
            _deleterService = tournamentDeleterService;
            _getterService = getterService;
            _adderService = tournamentAdderService;
            _updaterService = tournamentUpdaterService;
        }

        [HttpGet("AllTournamnets")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<TournamentResponseDto>>> GetAllTournaments()
        {
            var tournaments = await _getterService.GetAllTournaments();
            return Ok(tournaments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentResponseDto>> GetTournamentById(Guid id)
        {
            var tournament = await _getterService.GetTournamentById(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(tournament);
        }

        [HttpPost("CreateTournament")]
        public async Task<ActionResult<Tournament>> AddTournament(TournamentAddRequestDto tournamentAddRequestDto)
        {
            var addTournament = await _adderService.AddTournament(tournamentAddRequestDto);

            return Ok(addTournament);
        }
        [HttpPut("Update Tournament")]
        public async Task<ActionResult> UpdateTournament(TournamentUpdateRequestDto tournamentUpdateRequestDto)
        {
            var updatedTournament = await _updaterService.UpdateTournament(tournamentUpdateRequestDto);
            if (!updatedTournament)
            {
                return NotFound();
            }
            return Ok("Updated Successfully");
        }

        [HttpDelete("DeleteTournament")]
        public async Task<ActionResult> DeleteTournament(Guid id)
        {
            var isDeleted = await _deleterService.DeleteTournament(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok("Deleted successfully");
        }

        [HttpPost("AddUserToTournament")]
        public async Task<ActionResult> AddUserToTournament(AddUserToTournDto addUserToToTournamentDto)
        {
            var isAdded = await _updaterService.AddUserToTournament(addUserToToTournamentDto.TournamentId, addUserToToTournamentDto.Username);

            if (!isAdded)
            {
                return NotFound("User or tournament not found or user is already registered in the tournament");

            }
            return Ok("User added to the tournament");

        }

        [HttpPost("StartTournament")]
        public async Task<IActionResult> StartTournament(Guid tournamentId)
        {
            var success = await _updaterService.StartTournament(tournamentId);

            if (!success)
            {
                return NotFound("Tournament not found or not enough registered users to start the tournament.");

            }
            return Ok("Tournament started successfully.");
        }

        //[HttpPost("RemoveUserFromTournament")]
        //public async Task<IActionResult> UpdateTournament(AddUserToTournDto RemoveUserToFromTournamentDto)
        //{

            
        //    var result = await _updaterService.RemoveUserFromTournament(RemoveUserToFromTournamentDto.TournamentId, RemoveUserToFromTournamentDto.Username);

        //    if (!result)
        //    {
        //        return NotFound(); // Tournament with the provided ID was not found
        //    }

        //    return NoContent(); // Successfully updated the tournament
        //}



    }
}
