using System.Linq.Expressions;
using RubiconmpSolution.Business.Abstract;
using RubiconmpSolution.Business.Constants;
using RubiconmpSolution.Core.Utilities.Results;
using RubiconmpSolution.DataAccess.Abstract;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Business.Concrete;

public class UserService : IUserService
{
    private readonly IUserDal _userDal;
    
    public UserService(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public async Task<IDataResult<User>> GetByUsernameAsync(string username)
    {
        try
        {
            var user = await _userDal.GetAsync(u => u.Username == username);
            
            if(user is null)
                return new ErrorDataResult<User>(ErrorMessages.UserNotFound);

            return new SuccessDataResult<User>(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ErrorDataResult<User>(ErrorMessages.CommonError);
        }
    }

    public async Task<IDataResult<OperationClaim[]>> GetClaimsAsync(User user)
    {
        try
        {
            var claims = await _userDal.GetClaimsAsync(user);
            return new SuccessDataResult<OperationClaim[]>(claims);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ErrorDataResult<OperationClaim[]>(ErrorMessages.CommonError);
        }
    }

    public async Task<bool> AnyAsync(Expression<Func<User, bool>> filter)
    {
        return await _userDal.AnyAsync(filter);
    }

    public async Task<IDataResult<int>> CreateAsync(User user)
    {
        try
        {
            var count = await _userDal.CreateAsync(user);
            
            if(count > 0)
                return new SuccessDataResult<int>(count, SuccessMessages.SuccessfulUserCreation);

            return new ErrorDataResult<int>(count, ErrorMessages.FailedUserCreation);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ErrorDataResult<int>(ErrorMessages.CommonError);
        }
    }
}