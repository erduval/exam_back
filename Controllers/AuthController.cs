using Examen.DTO.Auth;
using Examen.Exceptions;
using Examen.Service.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Examen.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/tech")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterTech (RegisterDTO dto)
        {
            var result = await _authService.RegisterTechAsync(dto);
            if (!result.Succeeded)
            {
                throw new CustomErrorMessageException("error.creating.technician");
            }

            return Ok("Technicien registered successfully");
        }

        [HttpPost("register/client")]
        [Authorize(Roles = "Admin,Technicien")]
        public async Task<IActionResult> RegisterClient (RegisterDTO dto)
        {
            var result = await _authService.RegisterClientAsync(dto);
            if (!result.Succeeded)
            {
                throw new CustomErrorMessageException("error.creating.client");
            }

            return Ok("Client successfully created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null) throw new UnauthorizeException("credentials.error");
            return Ok(new { token });
        }
    }
}
