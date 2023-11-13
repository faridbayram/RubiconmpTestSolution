using System.Linq.Expressions;
using RubiconmpSolution.Core.Utilities.Results;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Business.Abstract;

public interface IUserService
{
    Task<IDataResult<User>> GetByUsernameAsync(string username);
    Task<IDataResult<OperationClaim[]>> GetClaimsAsync(User user);
    Task<bool> AnyAsync(Expression<Func<User, bool>> filter);
    Task<IDataResult<int>> CreateAsync(User user);
}