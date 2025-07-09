using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using UPC.FitWisePlatform.API.IAM.Application.Internal.OutboundServices;
using UPC.FitWisePlatform.API.IAM.Domain.Model.Aggregates;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Tokens.JWT.Configuration;

namespace UPC.FitWisePlatform.API.IAM.Infrastructure.Tokens.JWT.Services;

/**
 * <summary>
 *     The token service
 * </summary>
 * <remarks>
 *     This class is used to generate and validate tokens
 * </remarks>
 */
public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    /**
     * <summary>
     *     Generate token
     * </summary>
     * <param name="profile">The user for token generation</param>
     * <returns>The generated Token</returns>
     */
    public string GenerateToken(Profile profile)
    {
        var secret = _tokenSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, profile.Id.ToString()),
                new Claim(ClaimTypes.Name, profile.Username)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    /**
     * <summary>
     *     VerifyPassword token
     * </summary>
     * <param name="token">The token to validate</param>
     * <returns>The user id if the token is valid, null otherwise</returns>
     */
    public async Task<int?> ValidateToken(string token)
    {
        // If token is null or empty
        if (string.IsNullOrEmpty(token))
            // Return null 
            return null;
        // Otherwise, perform validation
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Expiration without delay
                ClockSkew = TimeSpan.Zero
            });

            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
            var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}