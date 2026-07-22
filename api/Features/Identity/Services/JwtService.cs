using System.Security.Claims;
using api.Features.Identity.Models;
using api.Settings;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace api.Features.Identity.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public string GenerateAccessToken(AppUser user)
        {
            Claim[] claims =
            [
                new (ClaimTypes.NameIdentifier, user.Id!.ToString()),
                new (ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email)
            ];

            SymmetricSecurityKey securityKey = new(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            
            SigningCredentials signingCredentials = new(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new();

            return tokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}