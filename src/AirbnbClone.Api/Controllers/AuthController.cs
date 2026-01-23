using AirbnbClone.Api.Contracts;
using AirbnbClone.Api.Data;
using AirbnbClone.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;

    public AuthController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Email and password are required." });

        var exists = await _db.Users.AnyAsync(u => u.Email == email);
        if (exists)
            return Conflict(new { message = "Email already registered." });

        var passwordHash = Sha256(request.Password);

        var user = new User
        {
            Email = email,
            PasswordHash = passwordHash
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return Created($"/users/{user.Id}", new
        {
            user.Id,
            user.Email,
            user.CreatedAtUtc
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthLoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(request.Password))
        return BadRequest(new { message = "Email and password are required." });

        var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user is null)
            return Unauthorized(new { message = "Invalid credentials." });

        var passwordHash = Sha256(request.Password);
            if (!string.Equals(user.PasswordHash, passwordHash, StringComparison.Ordinal))
            return Unauthorized(new { message = "Invalid credentials." });

        return Ok(new
    {
            user.Id,
            user.Email,
            user.CreatedAtUtc
        });
    }


    private static string Sha256(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes);
    }
}
