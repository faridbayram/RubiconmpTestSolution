using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Core.Utilities.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, OperationClaim[] operationClaims,bool rememberMe);
}