using Microsoft.AspNetCore.Mvc;
using RubiconmpSolution.Business.Abstract;
using RubiconmpSolution.Core.Entities.DTO.Auth;

namespace RubiconmpSolution.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);

            if (!loginResult.Success)
                return BadRequest(loginResult.Message);

            return Ok(loginResult.Data);
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var registrationResult = await _authService.Register(registerDto);
            
            if (!registrationResult.Success)
                return BadRequest(registrationResult.Message);

            return Ok(registrationResult.Data);
        }
    }
}
