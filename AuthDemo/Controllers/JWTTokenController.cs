using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public JWTTokenController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("GetToken", Name = "GetToken")]
        public async Task<string> GenerateToken()
        {
            return _jwtTokenService.GenerateToken();
        }

        [HttpGet("ValidateToken", Name = "ValidateToken")]
        public ActionResult ValidateToken(string token)
        {
            var jwtToken = _jwtTokenService.ValidateToken(token);
            if (jwtToken == null)
                return BadRequest("Invalid token");

            return Ok(jwtToken);
        }
    }
}
