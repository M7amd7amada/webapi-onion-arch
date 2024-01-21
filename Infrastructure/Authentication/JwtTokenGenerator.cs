using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Authentication;
using Application.Common.Interfaces.Services;

using Domain.Users;

using Infrastructure.Settings;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly AppSettings _appSettings;
    private readonly JwtSettings _jwtSettings;
    public JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider,
        IOptions<AppSettings> settingsOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _appSettings = settingsOptions.Value;
        _jwtSettings = _appSettings.JwtSettings!;
    }

    public string GenerateToken(User user)
    {
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: SetupClaims(user),
            signingCredentials: SetupSigningCredentials());

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    private static Claim[] SetupClaims(User user)
    {
        return
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];
    }

    private SigningCredentials SetupSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
    }
}