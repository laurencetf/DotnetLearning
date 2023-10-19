using DotnetBasics.API.Bowling.Features;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using DotnetBasics.Application.Features.BowlingThrows.Queries;
using MediatR;
using Moq;

namespace DotnetBasics.API.Test.Features;

public class BowlingControllerTest
{
    private readonly Mock<IMediator> _mediaTr;
    private readonly BowlingController _controller;

    public BowlingControllerTest()
    {
        _mediaTr = new Mock<IMediator>();
        _controller = new BowlingController(_mediaTr.Object);
    }

    [Fact]
    public async void GetBowlingThrows_WithQueryParamsShouldReturnBowlingList()
    {
        var queryParams = new GetBowlingThrowsQuery();
        var cancellationToken = new CancellationToken();

        _mediaTr
            .Setup(x => x.Send(It.IsAny<GetBowlingThrowsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new BowlingThrowList());

        var result = await _controller.GetBowlingThrows(queryParams, cancellationToken);

        Assert.IsType<BowlingThrowList>(result);
        _mediaTr.Verify(m => m.Send(queryParams, cancellationToken), Times.Once());
    }

    [Fact]
    public void GetBowlingThrows_WithQueryParamsShouldCatchException()
    {
        var queryParams = new GetBowlingThrowsQuery();
        var cancellationToken = new CancellationToken();

        _mediaTr
            .Setup(x => x.Send(It.IsAny<GetBowlingThrowsQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        Assert.ThrowsAsync<Exception>(async () =>
            await _controller.GetBowlingThrows(queryParams, cancellationToken)
        );
    }
}
