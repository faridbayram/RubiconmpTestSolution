using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RubiconmpSolution.Core.Extensions;
using RubiconmpSolution.Core.Utilities.Security.Encryption;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Core.Utilities.Security.JWT;

public class JwtHelper : ITokenHelper
{
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;
    
    public JwtHelper(IConfiguration configuration)
    {
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }
    
    public AccessToken CreateToken(User user, OperationClaim[] operationClaims, bool rememberMe)
    {
        _accessTokenExpiration = rememberMe ? DateTime.MaxValue : DateTime.Now.AddDays(_tokenOptions.ExpireAfterDays);
        
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(user, signingCredentials, operationClaims);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);
        
        return new AccessToken()
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }
    
    
    private JwtSecurityToken CreateJwtSecurityToken(User user, 
        SigningCredentials signingCredentials, OperationClaim[] operationClaims)
    {
        var jwt = new JwtSecurityToken(
            issuer:_tokenOptions.Issuer,
            audience:_tokenOptions.Audience,
            expires:_accessTokenExpiration,
            notBefore:DateTime.Now,
            claims: SetClaims(user,operationClaims),
            signingCredentials:signingCredentials
        );
        return jwt;
    }
    
    private IEnumerable<Claim> SetClaims(User user, OperationClaim[] operationClaims)
    {
        var claims = new List<Claim>();
        
        claims.AddUserId(user.Id);
        claims.AddRoles(operationClaims);
            
        return claims;
    }
}