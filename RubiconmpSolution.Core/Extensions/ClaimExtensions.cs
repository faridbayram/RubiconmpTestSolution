using System.Collections.Generic;
using System.Security.Claims;
using RubiconmpSolution.Core.Utilities.Custom;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Core.Extensions;

public static class ClaimExtensions
{
    public static void AddUsername(this ICollection<Claim> claims, string username)
    {
        claims.Add(new Claim(ClaimTypes.Name, username));
    }

    public static void AddUserId(this ICollection<Claim> claims, long userId)
    {
        claims.Add(new Claim(CustomClaims.UserId, userId.ToString()));
    }
    
    public static void AddRoles(this ICollection<Claim> claims, OperationClaim[] roles)
    {
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
    }
}