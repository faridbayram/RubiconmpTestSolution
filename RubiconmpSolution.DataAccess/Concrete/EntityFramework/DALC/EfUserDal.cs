using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RubiconmpSolution.DataAccess.Abstract;
using RubiconmpSolution.DataAccess.Concrete.EntityFramework.Contexts;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.DataAccess.Concrete.EntityFramework.DALC;

public class EfUserDal : EfEntityRepositoryBase<User, ApplicationDbContext>, IUserDal
{
    public EfUserDal(ApplicationDbContext dbContext) : base(dbContext)
    { }

    public async Task<OperationClaim[]> GetClaimsAsync(User user)
    {
        var userClaimsQueryable = 
            from operationClaim in _dbContext.OperationClaims
            join userOperationClaim in _dbContext.UserOperationClaims on operationClaim.Id equals userOperationClaim.OperationClaim.Id
            where userOperationClaim.User.Id == user.Id
            select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
        
        return await userClaimsQueryable.ToArrayAsync();
    }
}