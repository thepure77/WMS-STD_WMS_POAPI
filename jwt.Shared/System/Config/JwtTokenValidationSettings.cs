using System;
using System.Collections.Generic;
using System.Text;

namespace jwt.Shared.System.Config
{
    public class JwtTokenValidationSettings
    {
        public string ValidIssuer { get; set; }
        public bool ValidateIssuer { get; set; }

        public string ValidAudience { get; set; }
        public bool ValidateAudience { get; set; }

        public string SecretKey { get; set; }
    }
}
