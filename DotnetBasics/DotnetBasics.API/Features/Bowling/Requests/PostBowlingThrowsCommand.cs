using MediatR;

namespace DotnetBasics.API.Features.Bowling.Requests;

public record PostBowlingThrowsCommand
(
    int Pins,
    string PlayerName
): IRequest<bool>;