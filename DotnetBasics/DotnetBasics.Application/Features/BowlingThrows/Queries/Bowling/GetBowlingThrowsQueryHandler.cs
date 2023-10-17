using DotnetBasics.API.Bowling.Feature;
using DotnetBasics.API.Features.Bowling.Responses;
using DotnetBasics.Application.Abstraction.Interfaces;
using DotnetBasics.Application.Features.BowlingThrows.Mappers;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using MediatR;

namespace DotnetBasics.Application.Features.BowlingThrows.Queries.Bowling;

public class GetBowlingThrowsQueryHandler : IRequestHandler<GetBowlingThrowsQuery, BowlingThrowsPaginatedViewModel>
{
    
    public readonly IBowlingThrowsRepository _bowlingThrowsRepository;

    public GetBowlingThrowsQueryHandler(IBowlingThrowsRepository bowlingThrowsRepository)
    {
        _bowlingThrowsRepository = bowlingThrowsRepository;
    }


    public async Task<BowlingThrowsPaginatedViewModel> Handle(GetBowlingThrowsQuery request, CancellationToken cancellationToken)
    {
        
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
        return bowlingThrows
            .Select(bowlingThrow => BowlingThrow.FromDto(bowlingThrow))
            .ToPaginatedViewModel(request.PageSize, request.PageIndex);
    }
}