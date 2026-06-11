using KYC.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace KYC.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Hardcoded for now - real project would check DB
            if (request.Email == "omkar@test.com" && request.Password == "password123")
            {
                var token = _tokenService.GenerateToken("1", request.Email);
                return Ok(new { token = token });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }
    }

    public class LoginRequest
    {
        public string ? Email { get; set; }
        public string ?Password { get; set; }
    }


}

