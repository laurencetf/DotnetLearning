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
        FilterDefinition<BowlingThrowDto> filter,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    ) {
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

    public async Task UpsertBowlingThrow(BowlingThrowDto bowlingThrowDto)
    {
        var builder = Builders<BowlingThrowDto>.Update;
        var updateDefaultDefinition = Builders<BowlingThrowDto>.Update;

        var updates = new List<UpdateDefinition<BowlingThrowDto>>();

        var updateOptions = new UpdateOptions { IsUpsert = true };
        updates.Add(
            updateDefaultDefinition
                .SetOnInsert(p => p.Id, bowlingThrowDto.Id)
                .SetOnInsert(p => p.CreationDate, bowlingThrowDto.CreationDate)
                .Set(p => p.Date, bowlingThrowDto.Date)
                .Set(p => p.Status, bowlingThrowDto.Status)
                .Set(p => p.Pins, bowlingThrowDto.Pins)
                .Set(p => p.Player, bowlingThrowDto.Player)
        );

        await _collection.UpdateOneAsync(p => p.Id == bowlingThrowDto.Id, builder.Combine(updates), updateOptions).ConfigureAwait(false);
    }
}