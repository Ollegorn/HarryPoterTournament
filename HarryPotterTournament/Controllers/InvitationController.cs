using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces.InvitationInterfaces;
using ServiceContracts.InvitationDto;

namespace HarryPotterTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationGetterService _getterService;
        private readonly IInvitationAdderService _adderService;
        private readonly IInvitationDeleterService _deleterService;
        private readonly IInvitationUpdaterService _updaterService;

        public InvitationController(IInvitationGetterService getterService, IInvitationAdderService adderService, IInvitationDeleterService deleterService, IInvitationUpdaterService updaterService)
        {
            _getterService = getterService;
            _adderService = adderService;
            _deleterService = deleterService;
            _updaterService = updaterService;
        }

        [HttpGet("AllInvitations")]
        public async Task<ActionResult<List<Invitation>>> GetAllInvitations()
        {
            var invitations = await _getterService.GetAllInvitations();
            return Ok(invitations);
        }

        [HttpPost("AddInvitation")]
        public async Task<ActionResult<Invitation>> AddInvitation(InvitationAddRequestDto invitationAddRequestDto)
        {
            var addInvitation = await _adderService.AddInvitation(invitationAddRequestDto);
            return Ok(addInvitation);
        }

        [HttpDelete("DeleteInvitation")]
        public async Task<ActionResult> DeleteInvitation(Guid id)
        {
            var isDeleted = await _deleterService.DeleteInvitationById(id);
            if (!isDeleted)
                return NotFound();

            return Ok("Deleted successfully");
        }

        [HttpPost("ReturnInvitationToSender")]
        public async Task<ActionResult> UpdateInvitation(InvitationUpdateRequestDto invitationUpdateRequestDto)
        {
            var updatedInvitation = await _updaterService.ReturnInvitationToSender(invitationUpdateRequestDto);

            if (!updatedInvitation)
                return NotFound();

            return Ok("Updated Successfully");
        }

    }
}
