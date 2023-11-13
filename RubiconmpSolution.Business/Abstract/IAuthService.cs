using RubiconmpSolution.Core.Entities.DTO.Auth;
using RubiconmpSolution.Core.Utilities.Results;
using RubiconmpSolution.Core.Utilities.Security.JWT;

namespace RubiconmpSolution.Business.Abstract;

public interface IAuthService
{
    Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto);
    Task<IDataResult<AccessToken>> Register(RegisterDto registerDto);
}