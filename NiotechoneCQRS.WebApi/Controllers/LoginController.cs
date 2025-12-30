using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.DTOs.RequestDTOs;
using NiotechoneCQRS.Application.Login.Update;
using NiotechoneCQRS.Application.User.Commands.CheckUserValidatity;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Utility.AppResource;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NiotechoneCQRS.WebApi.Controllers;

public class LoginController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IConfiguration _configuration;

    public LoginController(ISender sender, IConfiguration configuration)
    {
        _sender = sender;
        _configuration = configuration;
    }

    [HttpPost(ApiRoutes.Login)]
    public async Task<IActionResult> Login(LoginRequestDTO login)
    {
        var userResponse = await _sender.Send(new CheckUserValidatityCommand(login));

        if (userResponse.Data == null)
            return BadRequest(new { Message = AppResource.LoginError });

        var user = userResponse.Data as User;
        if (user == null)
            return BadRequest(new { Message = AppResource.LoginError });

        // Generate JWT
        var token = GenerateJwtToken(user, out DateTime expiry);

        // Save token or last login time in User table
        await _sender.Send(new UpdateUserTokenCommand
        {
            UserId = user.UserId,
            JwtToken = token,
            TokenExpiry = expiry
        });

        var response = new TokenResponse
        {
            Token = token,
            TokenType = "Bearer",
            Message = AppResource.LoginSuccess,
            Expires = expiry
        };

        return Ok(response);
    }


    private string GenerateJwtToken(User user, out DateTime expiry)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email!),
            new Claim("UserRoleId", (user.UserRoleId?.ToString() ?? string.Empty)),
            new Claim("UserId", user.UserId.ToString())
        };

        // Ensure the secret key is at least 32 bytes (256 bits) for HS256
        var secretKey = jwtSettings["SecretKey"]!;
        if (string.IsNullOrWhiteSpace(secretKey) || Encoding.UTF8.GetBytes(secretKey).Length < 32)
        {
            throw new InvalidOperationException("JWT SecretKey must be at least 32 bytes (256 bits) for HS256.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        expiry = DateTime.UtcNow.AddHours(int.Parse(jwtSettings["ExpiryInHours"]!));

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
