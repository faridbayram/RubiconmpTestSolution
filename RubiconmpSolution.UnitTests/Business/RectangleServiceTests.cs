using Moq;
using RubiconmpSolution.Business.Concrete;
using RubiconmpSolution.Business.Constants;
using RubiconmpSolution.Core.Utilities.Results;
using RubiconmpSolution.Core.Utilities.ThirdPartyLibraries.FakeData.Concrete;
using RubiconmpSolution.DataAccess.Abstract;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.UnitTests.Business;

public class RectangleServiceTests
{
    [Fact]
    public async Task GetRectanglesAsync_CoordinatesInputIsNull_WritesToConsoleAndReturnsErrorDataResultWithProperMessage()
    {
        // arrange
        var consoleMock = new Mock<TextWriter>();
        Console.SetOut(consoleMock.Object);
        
        var sut = new RectangleService(It.IsAny<IRectangleDal>());

        // act
        var result = await sut.GetRectanglesAsync(null);

        // assert
        consoleMock.Verify(v => v.WriteLine(It.IsAny<ArgumentNullException>()), Times.Once);

        Assert.IsType<ErrorDataResult<Dictionary<Coordinate, Rectangle[]>>>(result);
        Assert.Equal(ErrorMessages.InputIsNull, result.Message);
    }
    
    [Fact]
    public async Task GetRectanglesAsync_CoordinatesInputIsEmpty_WritesToConsoleAndReturnsErrorDataResultWithProperMessage()
    {
        // arrange
        var consoleMock = new Mock<TextWriter>();
        Console.SetOut(consoleMock.Object);
        
        var sut = new RectangleService(It.IsAny<IRectangleDal>());

        // act
        var result = await sut.GetRectanglesAsync(Array.Empty<Coordinate>());

        // assert
        consoleMock.Verify(v => v.WriteLine(It.IsAny<InvalidOperationException>()), Times.Once);

        Assert.IsType<ErrorDataResult<Dictionary<Coordinate, Rectangle[]>>>(result);
        Assert.Equal(ErrorMessages.InputIsEmpty, result.Message);
    }

    [Fact]
    public async Task GetRectanglesAsync_NoProperRectanglesFoundAtDb_ReturnsErrorDataResultWithProperMessage()
    {
        // arrange
        var rectangleDalMock = new Mock<IRectangleDal>();
        rectangleDalMock.Setup(m => m.GetAllMatchingRectanglesAsync(
                It.IsAny<Coordinate>(),
                It.IsAny<Coordinate>()))
            .ReturnsAsync(Array.Empty<Rectangle>());

        var dataFaker = new BogusDataFaker();
        
        var sut = new RectangleService(rectangleDalMock.Object);

        // act
        var result = await sut.GetRectanglesAsync(dataFaker.GetFakeCoordinateData(5).ToArray());

        // assert
        Assert.IsType<ErrorDataResult<Dictionary<Coordinate, Rectangle[]>>>(result);
        Assert.Equal(ErrorMessages.NoRectanglesFound, result.Message);
    }

    [Fact]
    public async Task GetRectanglesAsync_SuccessCase_()
    {
        // Arrange
        var coordinates = new[]
        {
            new Coordinate() { X = 1, Y = 4 },
            new Coordinate() { X = 3, Y = 4 }
        };

        var rectangles = new Rectangle[]
        {
            new() { TopLeftCoordinateX = 0, TopLeftCoordinateY = 5, BottomRightCoordinateX = 2, BottomRightCoordinateY = 3 },
            new() { TopLeftCoordinateX = 2, TopLeftCoordinateY = 4, BottomRightCoordinateX = 4, BottomRightCoordinateY = 2 }
        };

        var mockRectangleDal = new Mock<IRectangleDal>();
        mockRectangleDal.Setup(dal => dal.GetAllMatchingRectanglesAsync(
                It.IsAny<Coordinate>(), 
                It.IsAny<Coordinate>()))
            .ReturnsAsync(rectangles);

        var sut = new RectangleService(mockRectangleDal.Object);

        // Act
        var result = await sut.GetRectanglesAsync(coordinates);

        // Assert
        Assert.IsType<SuccessDataResult<Dictionary<Coordinate, Rectangle[]>>>(result);

        var resultData = result.Data;
        Assert.NotNull(resultData);

        foreach (var coordinate in coordinates)
        {
            Assert.True(resultData.ContainsKey(coordinate));
            
            var rectanglesForCurrentCoordinate = resultData[coordinate];
            Assert.NotNull(rectanglesForCurrentCoordinate);
            Assert.NotEmpty(rectanglesForCurrentCoordinate);
        }
        
        mockRectangleDal.Verify(
            dal => dal.GetAllMatchingRectanglesAsync(It.IsAny<Coordinate>(), It.IsAny<Coordinate>()),
            Times.Once
        );
    }
}