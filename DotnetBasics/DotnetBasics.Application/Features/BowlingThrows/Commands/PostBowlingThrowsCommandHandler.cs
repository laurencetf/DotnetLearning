using DotnetBasics.Application.Features.BowlingThrows.Abstraction.Interfaces;
using DotnetBasics.Application.Features.BowlingThrows.Mappers;
using MediatR;

namespace DotnetBasics.Application.Features.BowlingThrows.Commands;

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
        var bowlingThrow = BowlingThrowsMapper.ToBowlingThrowCreation(command);

        await _bowlingThrowsRepository.UpsertBowlingThrow(bowlingThrow);

        return true;
    }
}
