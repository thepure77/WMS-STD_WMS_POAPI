using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace jwt.Shared.System.Config
{
    public class JwtTokenValidationSettingsFactory : IJwtTokenValidationSettings
    {
        private readonly JwtTokenValidationSettings settings;

        public string ValidIssuer => settings.ValidIssuer;
        public bool ValidateIssuer => settings.ValidateIssuer;
        public string ValidAudience => settings.ValidAudience;
        public bool ValidateAudience => settings.ValidateAudience;
        public string SecretKey => settings.SecretKey;

        public JwtTokenValidationSettingsFactory(IOptions<JwtTokenValidationSettings> options)
        {
            settings = options.Value;
        }

        public TokenValidationParameters CreateTokenValidationParameters()
        {
            var signingKey = Convert.FromBase64String(SecretKey);

            var result = new TokenValidationParameters
            {
                ValidateIssuer = ValidateIssuer,
                ValidIssuer = ValidIssuer,

                ValidateAudience = ValidateAudience,
                ValidAudience = ValidAudience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(signingKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            return result;
        }
    }
}
