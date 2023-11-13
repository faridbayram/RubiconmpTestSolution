using RubiconmpSolution.Core.Utilities.Results;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Business.Abstract;

public interface IRectangleService
{
    Task<IDataResult<Dictionary<Coordinate, Rectangle[]>>> GetRectanglesAsync(Coordinate[] coordinates);
}