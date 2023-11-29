using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.authentication;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;

    public AuthController(IConfiguration configuration, IAuthService authService) {
        _configuration = configuration;
        _authService = authService;
    }

    [HttpPost, Route("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto) {
        try {
            UserDto userDto = await _authService.ValidateUser(loginDto);
            string token = GenerateJwt(userDto);
            return Ok(token);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult<string>> Register([FromBody] UserDto userDto) {
        try {
            UserDto registeredUser = await _authService.RegisterUser(userDto);
            string token = GenerateJwt(registeredUser);
            return Ok(token);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }


    private string GenerateJwt(UserDto userDto) {
        List<Claim> claims = GenerateClaims(userDto);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));
        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        JwtHeader header = new JwtHeader(signingCredentials);

        JwtPayload payload = new JwtPayload(
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddHours(2));

        JwtSecurityToken token = new JwtSecurityToken(header, payload);
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }

    private List<Claim> GenerateClaims(UserDto userDto) {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Email", userDto.Email),
        };
        return claims.ToList();
    }
}