using System;

namespace lab.azure_active_directory_auth.JwtGenerator
{
    /// <summary>
    /// Jwt Token Options model.
    /// </summary>
    public class JwtTokenOptions
    {
        public const string Name = "JwtToken";

        public string ValidAudience { get; set; } = String.Empty;
        public string ValidIssuer { get; set; } = String.Empty;
        public string Secret { get; set; } = String.Empty;
        public int TokenValidityInHour { get; set; } = 10000;
        public int RefreshTokenValidityInHour { get; set; } = 10000;
    }
}
