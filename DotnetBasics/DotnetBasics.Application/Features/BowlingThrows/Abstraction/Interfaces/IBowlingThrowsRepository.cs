using DotnetBasics.Application.Features.BowlingThrows.Models;
using MongoDB.Driver;

namespace DotnetBasics.Application.Features.BowlingThrows.Abstraction.Interfaces;

public interface IBowlingThrowsRepository
{
    public Task<IEnumerable<BowlingThrow>> GetBowlingThrows(
        FilterDefinition<BowlingThrow> filter, 
        int pageIndex, 
        int pageSize,
        CancellationToken cancellationToken
    );

    public Task UpsertBowlingThrow(
        BowlingThrow bowlingThrow
    );
}