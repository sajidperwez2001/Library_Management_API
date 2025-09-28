using Microsoft.AspNetCore.Mvc;
using Library.Api.Services;
using Library.Api.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly ITokenService _token;
    public AuthController(IAuthService auth, ITokenService token)
    {
        _auth = auth; _token = token;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var user = await _auth.AuthenticateAsync(req.Username, req.Password);
        if (user == null) return Unauthorized();
        var t = _token.CreateToken(user);
        return Ok(new { token = t });
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var u = await _auth.RegisterAsync(req.Username, req.Password, req.Role);
        if (u == null) return BadRequest(new { message = "Cannot register" });
        return Ok();
    }
}
