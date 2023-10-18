namespace DotnetBasics.Application.Features.BowlingThrows.Commands;

using MediatR;

public record PostBowlingThrowsCommand
(
    int Pins,
    string PlayerName
): IRequest<bool>;