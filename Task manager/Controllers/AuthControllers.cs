using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Task_manager.Models;
using Task_manager.DTOs;

namespace Task_manager.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]

  public class AuthController : ControllerBase
  {
        private readonly SignInManager<Users> _signInManager;

        public AuthController(SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
        }

        // POST: auth/logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logged out successfully" });
        }
  }
}