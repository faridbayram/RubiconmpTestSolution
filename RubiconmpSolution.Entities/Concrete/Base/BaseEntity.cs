using RubiconmpSolution.Entities.Abstract;

namespace RubiconmpSolution.Entities.Concrete.Base;

public class BaseEntity : IEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.Now;
    }
    
    public long Id { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool? IsActive { get; set; }
}