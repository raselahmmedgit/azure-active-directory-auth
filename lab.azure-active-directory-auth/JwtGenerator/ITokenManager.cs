using System.Security.Claims;

namespace lab.azure_active_directory_auth.JwtGenerator
{
    public interface ITokenManager
    {
        Task<TokenModel> CreateTokenAsync(string loginId);
        RefreshTokenModel GenerateRefreshToken();
        ClaimsPrincipal GetClaimsPrincipalByToken(string token);
        bool IsValidateToken(string token);
    }
}
