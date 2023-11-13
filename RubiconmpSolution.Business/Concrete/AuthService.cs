using RubiconmpSolution.Business.Abstract;
using RubiconmpSolution.Business.Constants;
using RubiconmpSolution.Core.Entities.DTO.Auth;
using RubiconmpSolution.Core.Utilities.Results;
using RubiconmpSolution.Core.Utilities.Security.Hashing;
using RubiconmpSolution.Core.Utilities.Security.JWT;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Business.Concrete;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;

    public AuthService(IUserService userService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    public async Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto)
    {
        var getUserResult = await _userService.GetByUsernameAsync(loginDto.Username);

        if (!getUserResult.Success)
            return new ErrorDataResult<AccessToken>(getUserResult.Message);

        var user = getUserResult.Data;
        
        if (!HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            return new ErrorDataResult<AccessToken>(ErrorMessages.IncorrectPassword);

        var createTokenResult = await CreateAccessTokenAsync(user, loginDto.RememberMe);
        if (!createTokenResult.Success)
            return new ErrorDataResult<AccessToken>(ErrorMessages.CouldNotCreateAccessToken);

        return new SuccessDataResult<AccessToken>(createTokenResult.Data, SuccessMessages.AccessTokenCreated);
    }

    public async Task<IDataResult<AccessToken>> Register(RegisterDto registerDto)
    {
        try
        {
            var anyExistWithCurrentUsername = await _userService.AnyAsync(u => u.Username == registerDto.Username);

            if (anyExistWithCurrentUsername)
                return new ErrorDataResult<AccessToken>(ErrorMessages.UsernameAlreadyExist);

            HashingHelper.CreatePasswordHash(registerDto.Password, out var passwordHash, out var passwordSalt);

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            
            var createUserResult = await _userService.CreateAsync(user);
            if (createUserResult.Data == 0)
                return new ErrorDataResult<AccessToken>(ErrorMessages.UserCouldNotBeCreated);
            
            var createTokenResult = await CreateAccessTokenAsync(user, false);
            if (!createTokenResult.Success)
                return new ErrorDataResult<AccessToken>(ErrorMessages.CouldNotCreateAccessToken);

            return new SuccessDataResult<AccessToken>(createTokenResult.Data, SuccessMessages.AccessTokenCreated);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ErrorDataResult<AccessToken>(ErrorMessages.CommonError);
        }
    }
    
    private async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user, bool rememberMe)
    {
        try
        {
            var getClaimsResult = await _userService.GetClaimsAsync(user);

            if (!getClaimsResult.Success)
                return new ErrorDataResult<AccessToken>(ErrorMessages.CouldNotGetUserClaims);

            var accessToken = _tokenHelper.CreateToken(user, getClaimsResult.Data, rememberMe);

            return new SuccessDataResult<AccessToken>(accessToken, SuccessMessages.AccessTokenCreated);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ErrorDataResult<AccessToken>(ErrorMessages.CouldNotCreateAccessToken);
        }
    }
}