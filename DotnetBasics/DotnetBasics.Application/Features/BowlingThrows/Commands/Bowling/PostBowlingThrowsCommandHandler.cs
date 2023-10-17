using DotnetBasics.API.Features.Bowling.Requests;
using DotnetBasics.Application.Abstraction.Interfaces;
using DotnetBasics.Application.Features.BowlingThrows.Mappers;
using MediatR;

namespace DotnetBasics.Application;

public class PostBowlingThrowsCommandHandler : IRequestHandler<PostBowlingThrowsCommand, bool>
{
    private readonly IBowlingThrowsRepository _bowlingThrowsRepository;

    public PostBowlingThrowsCommandHandler (IBowlingThrowsRepository bowlingThrowsRepository)
    {
        _bowlingThrowsRepository = bowlingThrowsRepository;
    }

    public async Task<bool> Handle(
        PostBowlingThrowsCommand command,
        CancellationToken cancellationToken
    ) {
        var bowlingThrowDto = BowlingThrowsMapper.ToBowlingThrowCreationDto(command);

        await _bowlingThrowsRepository.UpsertBowlingThrow(bowlingThrowDto);

        return true;
    }
}
