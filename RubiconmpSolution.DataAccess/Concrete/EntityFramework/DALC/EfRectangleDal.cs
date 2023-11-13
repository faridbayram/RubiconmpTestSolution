using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RubiconmpSolution.DataAccess.Abstract;
using RubiconmpSolution.DataAccess.Concrete.EntityFramework.Contexts;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.DataAccess.Concrete.EntityFramework.DALC;

public class EfRectangleDal : EfEntityRepositoryBase<Rectangle, ApplicationDbContext>, IRectangleDal
{
    public EfRectangleDal(ApplicationDbContext dbContext) : base(dbContext)
    { }

    public async Task<Rectangle[]> GetAllMatchingRectanglesAsync(Coordinate topLeftOfSearchedArea, Coordinate bottomRightOfSearchedArea)
    {
        var result = await _dbContext.Rectangles
            .Where(f => f.TopLeftCoordinateX <= bottomRightOfSearchedArea.X)
            .Where(f => f.TopLeftCoordinateY >= bottomRightOfSearchedArea.Y)
            .Where(f => f.BottomRightCoordinateX >= topLeftOfSearchedArea.X)
            .Where(f => f.BottomRightCoordinateY <= topLeftOfSearchedArea.Y)
            .ToArrayAsync();
        
        return result;
    }
}