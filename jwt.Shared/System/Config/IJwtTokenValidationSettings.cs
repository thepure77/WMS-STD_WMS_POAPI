using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace jwt.Shared.System.Config
{
    public interface IJwtTokenValidationSettings
    {
        string ValidIssuer { get; }
        bool ValidateIssuer { get; }

        string ValidAudience { get; }
        bool ValidateAudience { get; }

        string SecretKey { get; }

        TokenValidationParameters CreateTokenValidationParameters();
    }
}
