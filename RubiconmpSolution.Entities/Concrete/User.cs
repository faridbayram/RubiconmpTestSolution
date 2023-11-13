using RubiconmpSolution.Entities.Concrete.Base;

namespace RubiconmpSolution.Entities.Concrete;

public class User : BaseEntity
{
    public string Username { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
}