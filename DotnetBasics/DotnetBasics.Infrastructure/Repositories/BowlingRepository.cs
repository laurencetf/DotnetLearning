using DotnetBasics.Application;
using DotnetBasics.Application.Abstraction.Interfaces;
using MongoDB.Driver;

namespace DotnetBasics.Infrastructure.Repositories;

public class BowlingRepository : IBowlingThrowsRepository
{
    
    private readonly IMongoCollection<BowlingThrowDto> _collection;

    public BowlingRepository(IMongoCollection<BowlingThrowDto> collection)
    {
        _collection = collection;
    }


    public async Task<IEnumerable<BowlingThrowDto>> GetBowlingThrows(
        FilterDefinition<BowlingThrowDto> filter, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        const int defaultStartIndex = 0;
        const int defaultPageSize = 10;

        var startIndex = Math.Max((pageIndex - 1) * pageSize, defaultStartIndex);
        var maxResult = pageSize > 0 ? pageSize : defaultPageSize;

        return await _collection
                .Find(filter)
                .Skip(startIndex)
                .Limit(maxResult)
                .SortByDescending(dto => dto.CreationDate)
                .ToListAsync(cancellationToken);
    }
}