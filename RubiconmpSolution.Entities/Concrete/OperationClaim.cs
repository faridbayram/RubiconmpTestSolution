using RubiconmpSolution.Entities.Concrete.Base;

namespace RubiconmpSolution.Entities.Concrete;

public class OperationClaim : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
}