using MongoDB.Driver;

namespace DotnetBasics.Application.Abstraction.Interfaces;

public interface IBowlingThrowsRepository
{
    public Task<IEnumerable<BowlingThrowDto>> GetBowlingThrows(
        FilterDefinition<BowlingThrowDto> filter, 
        int pageIndex, 
        int pageSize,
        CancellationToken cancellationToken
    );

    public Task UpsertBowlingThrow(
        BowlingThrowDto bowlingThrowDto
    );
}