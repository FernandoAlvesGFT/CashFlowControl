using CashFlowControl.Core.Application.DTOs;
using CashFlowControl.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowControl.Permissions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDTO model)
        {
            var (result, user) = await _authService.RegisterUser(model);

            if (result == null || !result.Succeeded)
                return BadRequest(result?.Errors);

            var accessToken = _authService.GenerateAccessToken(user.Id);
            var refreshToken = _authService.GenerateRefreshToken(user.Id);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
        {
            var (result, user) = await _authService.Authenticate(model);

            if (result == null || !result.Succeeded || user == null)
                return Unauthorized();

            var accessToken = _authService.GenerateAccessToken(user.Id);
            var refreshToken = _authService.GenerateRefreshToken(user.Id);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            try
            {
                var newAccessToken = _authService.RefreshAccessToken(request.RefreshToken);
                return Ok(new { AccessToken = newAccessToken });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
