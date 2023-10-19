using DotnetBasics.Application.Features.BowlingThrows.Abstraction.Interfaces;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotnetBasics.Infrastructure.Repositories;

public class BowlingRepository : IBowlingThrowsRepository
{
    
    private readonly IMongoCollection<BowlingThrow> _collection;

    public BowlingRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        _collection = new MongoClient(mongoDBSettings.Value.ConnectionURI)
            .GetDatabase(mongoDBSettings.Value.DatabaseName)
            .GetCollection<BowlingThrow>(mongoDBSettings.Value.CollectionName);
    }


    public async Task<IEnumerable<BowlingThrow>> GetBowlingThrows(
        FilterDefinition<BowlingThrow> filter,
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

    public async Task UpsertBowlingThrow(BowlingThrow bowlingThrowDto)
    {
        var builder = Builders<BowlingThrow>.Update;
        var updateDefaultDefinition = Builders<BowlingThrow>.Update;

        var updates = new List<UpdateDefinition<BowlingThrow>>();

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