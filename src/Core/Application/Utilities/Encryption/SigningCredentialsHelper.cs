﻿using Microsoft.IdentityModel.Tokens;

namespace Application.Utilities.Encryption;

public class SigningCredentialsHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }
}
