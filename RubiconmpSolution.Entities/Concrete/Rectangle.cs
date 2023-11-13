using RubiconmpSolution.Entities.Abstract;
using RubiconmpSolution.Entities.Concrete.Base;

namespace RubiconmpSolution.Entities.Concrete;

public class Rectangle : BaseEntity, IEntity
{
    public double TopLeftCoordinateX { get; set; }
    public double TopLeftCoordinateY { get; set; }
    public double BottomRightCoordinateX { get; set; }
    public double BottomRightCoordinateY { get; set; }
}