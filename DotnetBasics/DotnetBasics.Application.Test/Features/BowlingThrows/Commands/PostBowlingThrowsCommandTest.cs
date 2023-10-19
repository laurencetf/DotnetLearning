namespace DotnetBasics.Application.Test.Features.BowlingThrows.Commands;

using DotnetBasics.Application.Features.BowlingThrows.Abstraction.Interfaces;
using DotnetBasics.Application.Features.BowlingThrows.Commands;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using Moq;

public class PostBowlingThrowsCommandTest
{
    private readonly Mock<IBowlingThrowsRepository> _repository;
    private readonly PostBowlingThrowsCommandHandler _handler;

    public PostBowlingThrowsCommandTest()
    {
        _repository = new Mock<IBowlingThrowsRepository>();
        _handler = new PostBowlingThrowsCommandHandler(_repository.Object);
    }

    [Fact]
    public async void PostBowlingThrowsCommandHandler_HandleCommandShouldReturnTrue()
    {
        var command = new PostBowlingThrowsCommand(1, "name");
        var token = new CancellationToken();

        var result = await _handler.Handle(command, token);

        Assert.True(result);
        _repository.Verify(r => r.UpsertBowlingThrow(It.IsAny<BowlingThrow>()), Times.Once());
    }
}
