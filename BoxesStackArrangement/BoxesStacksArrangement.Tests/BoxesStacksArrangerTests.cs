using BoxesStackArrangement;

namespace BoxesStacksArrangement.Tests;

public class BoxesStacksArrangerTests
{

    private const int MinNumberOfStacks = 2;
    private const int MaxNumberOfStacks = 8;
    
    private const int MinNumberOfBoxes = 2;
    private const int MaxNumberOfBoxes = 16;
    
    private readonly Random _random;

    public BoxesStacksArrangerTests()
    {
        _random = new Random();
    }
    
    [Theory]
    [InlineData(MinNumberOfStacks)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(MaxNumberOfStacks)]
    public void WhenProvidingBoxesListWithNStacksOfOneBox_ShouldReturnTrue(int numberOfStacks)
    {
        //Arrange
        var boxesStacks = Enumerable.Repeat(1, numberOfStacks).ToArray();
        var clawPosition = _random.Next(numberOfStacks);
        
        //Act
        var result = BoxesStacksArranger.ArrangeBoxesStack(clawPosition, boxesStacks);
        
        //Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(MinNumberOfStacks)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(MaxNumberOfStacks)]
    public void WhenProvidingBoxesListWithOnlyFirstStackContainingBoxes_ShouldFillOtherStacksAndReturnTrue(int numberOfStacks)
    {
        //Arrange
        var boxesStacks = Enumerable.Repeat(0, numberOfStacks).ToArray();
        boxesStacks[0] = numberOfStacks;
        var clawPosition = _random.Next(boxesStacks.Length);
        
        //Act
        var result = BoxesStacksArranger.ArrangeBoxesStack(clawPosition, boxesStacks);
        
        //Assert
        Assert.True(result);
    }
    
    [Theory]
    [InlineData(MinNumberOfStacks)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(MaxNumberOfStacks)]
    public void WhenProvidingBoxesListWithRandomStacksFilled_ShouldFillOtherStacksAndReturnTrue(int numberOfStacks)
    {
        //Arrange
        var boxesStacks = Enumerable.Repeat(0, numberOfStacks).ToArray();
        boxesStacks[0] = numberOfStacks;
        boxesStacks[^1] = numberOfStacks;
        var clawPosition = _random.Next(boxesStacks.Length);
        
        //Act
        var result = BoxesStacksArranger.ArrangeBoxesStack(clawPosition, boxesStacks);
        
        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void WhenProvidingBoxesListWithCustomStacksFilled_ShouldFillOtherStacksAndReturnTrue()
    {
        //Arrange
        var boxesStacks = new []{ 0, 0, 0, 0, 0, 0, 0, 15 };

        //Act
        var result = BoxesStacksArranger.ArrangeBoxesStack(0, boxesStacks);
        
        //Assert
        Assert.True(result);
    }
    
    
}