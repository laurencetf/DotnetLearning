using DotnetBasics.Application.Features.BowlingThrows.Abstraction.Interfaces;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using DotnetBasics.Application.Features.BowlingThrows.Queries;
using Moq;

namespace DotnetBasics.Application.Test.Features.BowlingThrows.Queries;

public class GetBowlingThrowQueryHandlerTest
{
    private readonly Mock<IBowlingThrowsRepository> _repository;
    private readonly GetBowlingThrowsQueryHandler _handler;

    public GetBowlingThrowQueryHandlerTest()
    {
        _repository = new Mock<IBowlingThrowsRepository>();
        _handler = new GetBowlingThrowsQueryHandler(_repository.Object);
    }

    [Fact]
    public async void GetBowlingThrowQueryHandler_HandlerWithPageSizeShouldReturnLimitedList()
    {
        var request = new GetBowlingThrowsQuery
        {
            PageIndex = 1,
            PageSize = 5
        };
        var token = new CancellationToken();
        _repository.Setup(r => r.GetBowlingThrows(
            It.IsAny<MongoDB.Driver.FilterDefinition<BowlingThrow>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()
        ));

        var result = await _handler.Handle(request, token);

        Assert.IsType<BowlingThrowList>(result);
    }
}