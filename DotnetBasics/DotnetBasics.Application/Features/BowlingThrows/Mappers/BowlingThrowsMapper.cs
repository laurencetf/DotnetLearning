using DotnetBasics.Application.Features.BowlingThrows.Commands;
using DotnetBasics.Application.Features.BowlingThrows.Enums;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using DotnetBasics.Application.Features.BowlingThrows.Queries;
using MongoDB.Driver;

namespace DotnetBasics.Application.Features.BowlingThrows.Mappers;

public static class BowlingThrowsMapper
{
    public static BowlingThrowList ToBowlingThrowCollection (
        this IEnumerable<BowlingThrow> collection,
        GetBowlingThrowsQuery query
    ) {
        return new BowlingThrowList {
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Items = collection
        };
    }

    public static BowlingThrow ToBowlingThrowCreation(
        this PostBowlingThrowsCommand command
    ) {
        return new BowlingThrow {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Pins = command.Pins,
            Player = new Player {
                FirstName = command.PlayerName
            },
            Status = ThrowStatus.Thrown,
            CreationDate = DateTime.UtcNow
        };
    }

    public static FilterDefinition<BowlingThrow> ToDatabaseFilter(this GetBowlingThrowsQuery query)
    {
        
        var filterBuilder = Builders<BowlingThrow>.Filter;
        var filterDefinition = Builders<BowlingThrow>.Filter.Empty;
        

        if (query.Status.HasValue)
        {
            filterDefinition &= filterBuilder.Eq<string>(bowlingThrow => bowlingThrow.Status.ToString(), query.Status.ToString());
        }

        if (query.ThrowMinDate.HasValue)
        {
            filterDefinition &= filterBuilder.Gte(bowlingThrow => bowlingThrow.CreationDate, query.ThrowMinDate);
        }

        if (query.ThrowMaxDate.HasValue)
        {
            filterDefinition &= filterBuilder.Lte(bowlingThrow => bowlingThrow.CreationDate, query.ThrowMaxDate);
        }

        return filterDefinition;
    }
}