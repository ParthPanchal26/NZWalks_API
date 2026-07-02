using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO.AuthDTOs;
using NZWalks.API.Repositories.AuthRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        #region post

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if(identityResult.Succeeded)
            {
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User registered successfully, please sign-in");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LoginUser([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.UserName);

            if (user == null) return NotFound("User not found");

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isCorrectPassword) return BadRequest("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);

            if(roles == null) return BadRequest("Invalid credentials");

            var jwttoken = _tokenRepository.CreateJWTToken(user, roles.ToList());

            var JwtTokenResponse = new LoginResponseDTO
            {
                JwtToken = jwttoken
            };

            return Ok(JwtTokenResponse);
        }

        #endregion


    }
}
