using System.Collections.Generic;
using System.Threading.Tasks;
using RubiconmpSolution.Core.DataAccess;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.DataAccess.Abstract;

public interface IUserDal : IEntityRepository<User>
{
    Task<OperationClaim[]> GetClaimsAsync(User user);
}