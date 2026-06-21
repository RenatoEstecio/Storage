using Library.DTO;
using Library.Util;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerceApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var userAgent = Request.Headers.UserAgent.ToString();
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                var operatingSystem = UserAgentParser.ParseOperatingSystem(userAgent);

                var context = new RequestContext(ipAddress, operatingSystem, userAgent);

                var response = await _authService.LoginAsync(request, context);

                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
