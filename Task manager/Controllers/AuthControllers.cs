using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task_manager.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]

  public class AuthController : ControllerBase
  {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: auth/logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return NoContent();
        }
  }
}