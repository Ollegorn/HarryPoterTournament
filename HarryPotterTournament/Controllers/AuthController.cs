using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.AuthorizationDto;
using ServiceContracts.AuthorizationDTOs;
using ServiceContracts.Interfaces;
using Services;

namespace HarryPotterTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email
            };

            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (!result.Succeeded)
            {
            return BadRequest("Registration failed");

            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok("Registration successful");
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginRequestDto userLoginRequestDto)
        {

            //validate
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid information");
            }

            //check if user exists
            var existingUser = await _userManager.FindByEmailAsync(userLoginRequestDto.Email);
            if (existingUser == null)
            {
                return BadRequest("User doesn't exist");
            }

            //check if password is correct
            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, userLoginRequestDto.Password);
            if (!isCorrect)
            {
                return BadRequest("Wrong password");
            }
            //generate token
            var jwtToken = await _jwtService.GenerateJwtToken(existingUser);

            return Ok(jwtToken);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid parameters");
            }
            var result = await _jwtService.VerifyAndGenerateToken(tokenRequest);
            if (result == null)
            {

                return BadRequest("Invalid Tokens");
            }

            return Ok(result);
        }

    }
}
