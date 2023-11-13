using System.Collections.Generic;
using Bogus;
using RubiconmpSolution.Core.Utilities.ThirdPartyLibraries.FakeData.Abstract;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Core.Utilities.ThirdPartyLibraries.FakeData.Concrete;

public class BogusDataFaker : IDataFaker
{
    public List<Rectangle> GetFakeRectangleData(int count)
    {
        var rectangleId = 1;

        var faker = new Faker<Rectangle>()
            .RuleFor(r => r.Id, (f, r) => rectangleId++)
            .RuleFor(r => r.TopLeftCoordinateX, s => s.Random.Number(-100, 100))
            .RuleFor(r => r.TopLeftCoordinateY, s => s.Random.Number(-100, 100))
            .RuleFor(r => r.BottomRightCoordinateX, (f, r) => f.Random.Number((int)r.TopLeftCoordinateX, 100))
            .RuleFor(r => r.BottomRightCoordinateY, (f, r) => f.Random.Number(-100, (int)r.TopLeftCoordinateY));
        
        return faker.Generate(count);
    }

    public List<Coordinate> GetFakeCoordinateData(int count)
    {
        var faker = new Faker<Coordinate>()
            .RuleFor(r => r.X, s => s.Random.Number(-100, 100))
            .RuleFor(r => r.Y, s => s.Random.Number(-100, 100));

        return faker.Generate(count);
    }
}