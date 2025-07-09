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
        if (string.IsNullOrEmpty(token))
            return null;

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
                ClockSkew = TimeSpan.Zero
            });

            // Verificar si la validación fue exitosa antes de intentar acceder al token
            if (!tokenValidationResult.IsValid)
            {
                Console.WriteLine($"Token validation failed: {tokenValidationResult.Exception?.Message}");
                return null;
            }

            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;

            // ***** CAMBIO CLAVE AQUI *****
            // Usar FirstOrDefault() para evitar InvalidOperationException si el claim no existe
            // y luego TryParse para la conversión segura a int.
            var sidClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);

            if (sidClaim != null && int.TryParse(sidClaim.Value, out int userId))
            {
                return userId;
            }
            else
            {
                // Esto se ejecutará si el claim no se encuentra o no es un entero válido
                Console.WriteLine("Claim 'sid' not found or could not be parsed to integer.");
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An exception occurred during token validation: {e}");
            return null;
        }
    }
}