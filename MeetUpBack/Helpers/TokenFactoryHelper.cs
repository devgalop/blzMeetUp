using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetUpBack.Data.Entities;
using MeetUpBack.Models.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeetUpBack.Helpers;

public class TokenFactoryHelper : ITokenFactoryHelper
{
    private readonly JwtConfigModel _config;

    public TokenFactoryHelper(IOptions<JwtConfigModel> config)
    {
        _config = config.Value;
    }

    public TokenModel GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.Now.AddHours(12),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenCreated = tokenHandler.CreateToken(tokenDescriptor);
        var result = new {
            token = tokenHandler.WriteToken(tokenCreated),
            expiration = tokenCreated.ValidTo.ToLocalTime()
        };
        return new TokenModel()
        {
            Token = result.token,
            ExpiresIn = result.expiration
        };
    }
}