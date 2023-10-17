using DotnetBasics.API.Features.Bowling.Requests;
using DotnetBasics.API.Features.Bowling.Requests.Enum;
using DotnetBasics.API.Features.Bowling.Responses;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using MongoDB.Driver;

namespace DotnetBasics.Application.Features.BowlingThrows.Mappers;

public static class BowlingThrowsMapper
{
    public static BowlingThrowsPaginatedViewModel ToPaginatedViewModel(
        this IEnumerable<BowlingThrow> bowlingThrows,
        int pageSize,
        int pageIndex
    ) {
        return new BowlingThrowsPaginatedViewModel
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            Items = bowlingThrows.Select(bt => new BowlingThrowViewModel
            (
                bt.Id,
                bt.Pins,
                new PlayerViewModel
                (
                    bt.Player.FirstName,
                    bt.Player.LastName
                )   
            ))
        };
    }

    public static BowlingThrowDto ToBowlingThrowCreationDto(
        this PostBowlingThrowsCommand bowlingThrowsCommand
    ) {
        return new BowlingThrowDto {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Pins = bowlingThrowsCommand.Pins,
            Player = new PlayerDto {
                FirstName = bowlingThrowsCommand.PlayerName
            },
            Status = ThrowStatus.Thrown.ToString(),
            CreationDate = DateTime.UtcNow
        };
    }

    public static FilterDefinition<BowlingThrowDto> ToDatabaseFilter(this GetBowlingThrowsQuery query)
    {
        
        var filterBuilder = Builders<BowlingThrowDto>.Filter;
        var filterDefinition = Builders<BowlingThrowDto>.Filter.Empty;
        

        if (query.Status.HasValue)
        {
            filterDefinition &= filterBuilder.Eq<string>(bowlingThrow => bowlingThrow.Status, query.Status.ToString());
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