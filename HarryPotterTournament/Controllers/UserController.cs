using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.UserDto;
using Services.UserServices;
using System.Text.RegularExpressions;

namespace HarryPotterTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserGetterService _userGetterService;
        private readonly IUserUpdaterService _userUpdaterService;

        public UserController(IUserGetterService userGetterService, IUserUpdaterService userUpdaterService)
        {
            _userGetterService = userGetterService;
            _userUpdaterService = userUpdaterService;
        }

        [HttpGet("Username")]
        public async Task<ActionResult<User>> GetUserByUsername([FromHeader] string username)
        {
            var user = await _userGetterService.GetUserByUsername(username);

            return Ok(user);
        }
        //[HttpPut("updateUserPoints")]
        //public async Task<IActionResult> UpdateUserPoints([FromBody] UserUpdateRequestDto userUpdateRequestDto)
        //{
        //    if (userUpdateRequestDto == null)
        //    {
        //        return BadRequest("Invalid user update request.");
        //    }
            
        //    var result = await _userUpdaterService.UpdateUserPoints(userUpdateRequestDto);

        //    if (!result)
        //    {
        //        return NotFound("User not found or points update failed.");
        //    }
            
        //    return Ok("User points updated successfully.");

        //}

        [HttpGet("AllUsers")]
        public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
        {
            var users = await _userGetterService.GetAllUsers();
            return Ok(users);
        }

    }
}
