
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;

public interface IJwtTokenService
{
    string GenerateToken();
    JwtSecurityToken ValidateToken(string token);
}

public class JwtTokenService : IJwtTokenService
{
    private readonly string secret = "this is a secret key for auth demo app.";
    public string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.UTF8.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "issuer",
            Audience = "audience",
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test@email.com"),
                    // new Claim(ClaimTypes.Role, user.Role),
                // new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
            }),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public JwtSecurityToken ValidateToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.UTF8.GetBytes(secret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "issuer", // configuration[JwtSettings.SectionName + ":Issuer"],
                ValidAudience = "audience", // configuration[JwtSettings.SectionName + ":Audience"]
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            // if (jwtToken != null)
            // {
            //     var claims = new List<string>();
            //     foreach (var claim in jwtToken.Claims)
            //     {
            //         if (claim.Type.ToLower() == ClaimTypes.NameIdentifier)
            //         {
            //             claims.Add(claim.Value);
            //             Console.WriteLine(claim.Value);
            //         }
            //     }
            // }

            return jwtToken;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }
}
