using lab.azure_active_directory_auth.Core.Identity;
using lab.azure_active_directory_auth.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace lab.azure_active_directory_auth.JwtGenerator
{
    /// <summary>
    /// Token Generation middleware. Please do not change anything here.
    /// </summary>
    public class TokenManager : ITokenManager
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _appClaimsPrincipalFactory;

        public TokenManager(IOptions<JwtTokenOptions> jwtTokenOptions, IUserClaimsPrincipalFactory<ApplicationUser> appClaimsPrincipalFactory, UserManager<ApplicationUser> userManager)
        {
            _jwtTokenOptions = jwtTokenOptions.Value;
            _userManager = userManager;
            _appClaimsPrincipalFactory = appClaimsPrincipalFactory;
        }

        public async Task<TokenModel> CreateTokenAsync(string loginId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginId);
                if (user != null)
                {
                    //var authClaims = new[] {
                    //    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    //    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    //    new Claim(JwtRegisteredClaimNames.Jti, user.Id)
                    //};

                    var authClaims = await _appClaimsPrincipalFactory.CreateAsync(user);

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret));
                    var token = new JwtSecurityToken(
                        issuer: _jwtTokenOptions.ValidIssuer,
                        audience: _jwtTokenOptions.ValidAudience,
                        expires: DateTime.Now.AddHours(_jwtTokenOptions.RefreshTokenValidityInHour),
                        //claims: authClaims,
                        claims: authClaims.Claims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return new TokenModel
                    {
                        AccessToken = token,
                        ExpiryDate = DateTime.Now.AddHours(_jwtTokenOptions.RefreshTokenValidityInHour),
                        RefreshTokenModel = GenerateRefreshToken(),
                        AddedDate = DateTime.Now
                    };
                }
                return new TokenModel();
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public RefreshTokenModel GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var val = Convert.ToBase64String(randomNumber);
            return new RefreshTokenModel
            {
                RefreshToken = val,
                ExpiryDate = DateTime.Now.AddHours(_jwtTokenOptions.RefreshTokenValidityInHour),
                AddedDate = DateTime.Now
            };
        }

        public ClaimsPrincipal GetClaimsPrincipalByToken(string token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = _jwtTokenOptions.ValidAudience,
                    ValidIssuer = _jwtTokenOptions.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret)),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                { 
                    throw new SecurityTokenException(MessageHelper.JwtTokenInvalid); 
                }

                return principal;
            }
            catch (SecurityTokenException )
            {
                throw new SecurityTokenException(MessageHelper.JwtTokenInvalid);
            }
        }

        public bool IsValidateToken(string token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = _jwtTokenOptions.ValidAudience,
                    ValidIssuer = _jwtTokenOptions.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret)),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
