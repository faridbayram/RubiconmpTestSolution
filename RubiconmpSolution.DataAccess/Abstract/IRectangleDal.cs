using System.Threading.Tasks;
using RubiconmpSolution.Core.DataAccess;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.DataAccess.Abstract
{
    public interface IRectangleDal : IEntityRepository<Rectangle>
    {
        Task<Rectangle[]> GetAllMatchingRectanglesAsync(Coordinate topLeftOfSearchedArea, Coordinate bottomRightSearchedArea);
    }
}