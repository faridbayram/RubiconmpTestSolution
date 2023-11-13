using System.Collections.Generic;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Core.Utilities.ThirdPartyLibraries.FakeData.Abstract;

public interface IDataFaker
{
    List<Rectangle> GetFakeRectangleData(int count);
    List<Coordinate> GetFakeCoordinateData(int count);
}