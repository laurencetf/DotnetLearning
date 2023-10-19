using DotnetBasics.Application.Features.BowlingThrows.Abstraction.Interfaces;
using DotnetBasics.Application.Features.BowlingThrows.Mappers;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using MediatR;

namespace DotnetBasics.Application.Features.BowlingThrows.Queries;

public class GetBowlingThrowsQueryHandler : IRequestHandler<GetBowlingThrowsQuery, BowlingThrowList>
{
    private readonly IBowlingThrowsRepository _bowlingThrowsRepository;

    public GetBowlingThrowsQueryHandler(IBowlingThrowsRepository bowlingThrowsRepository)
    {
        _bowlingThrowsRepository = bowlingThrowsRepository;
    }


    public async Task<BowlingThrowList> Handle(
        GetBowlingThrowsQuery request,
        CancellationToken cancellationToken
    ) {
        //Mapping d'entrée
        var databaseFilter = request.ToDatabaseFilter();

        //Appel au repository
        var bowlingThrows = await _bowlingThrowsRepository.GetBowlingThrows(
            databaseFilter, 
            request.PageIndex, 
            request.PageSize, 
            cancellationToken
        );

        //Mapping de sortie
        return BowlingThrowsMapper.ToBowlingThrowCollection(bowlingThrows, request);
    }
}