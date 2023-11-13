using RubiconmpSolution.Business.Abstract;
using RubiconmpSolution.Business.Constants;
using RubiconmpSolution.Core.Utilities.Results;
using RubiconmpSolution.DataAccess.Abstract;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Business.Concrete;

public class RectangleService : IRectangleService
{
    private readonly IRectangleDal _rectangleDal;

    public RectangleService(IRectangleDal rectangleDal)
    {
        _rectangleDal = rectangleDal;
    }

    public async Task<IDataResult<Dictionary<Coordinate, Rectangle[]>>> GetRectanglesAsync(Coordinate[] coordinates)
    {
        try
        {
            var topLeftOfSearchedArea = new Coordinate();
            var bottomRightOfSearchedArea = new Coordinate();

            topLeftOfSearchedArea.X = coordinates.Min(c => c.X);
            topLeftOfSearchedArea.Y = coordinates.Max(c => c.Y);
            bottomRightOfSearchedArea.X = coordinates.Max(c => c.X);
            bottomRightOfSearchedArea.Y = coordinates.Min(c => c.Y);

            var rectanglesInRange =
                await _rectangleDal.GetAllMatchingRectanglesAsync(topLeftOfSearchedArea, bottomRightOfSearchedArea);

            if (!rectanglesInRange.Any())
                return new ErrorDataResult<Dictionary<Coordinate, Rectangle[]>>(ErrorMessages.NoRectanglesFound);

            var result = new Dictionary<Coordinate, Rectangle[]>();
            foreach (var coordinate in coordinates)
            {
                var rectanglesForCurrentCoordinate = rectanglesInRange
                    .Where(r => r.TopLeftCoordinateX <= coordinate.X && r.BottomRightCoordinateX >= coordinate.X)
                    .Where(r => r.TopLeftCoordinateY >= coordinate.Y && r.BottomRightCoordinateY <= coordinate.Y)
                    .ToArray();

                result.Add(coordinate, rectanglesForCurrentCoordinate);
            }

            return new SuccessDataResult<Dictionary<Coordinate, Rectangle[]>>(result);
        }
        catch (ArgumentNullException argumentNullException)
        {
            Console.WriteLine(argumentNullException);
            return new ErrorDataResult<Dictionary<Coordinate, Rectangle[]>>(ErrorMessages.InputIsNull);
        }
        catch (InvalidOperationException invalidOperationException)
        {
            Console.WriteLine(invalidOperationException);
            return new ErrorDataResult<Dictionary<Coordinate, Rectangle[]>>(ErrorMessages.InputIsEmpty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ErrorDataResult<Dictionary<Coordinate, Rectangle[]>>(ErrorMessages.CommonError);
        }
    }
}