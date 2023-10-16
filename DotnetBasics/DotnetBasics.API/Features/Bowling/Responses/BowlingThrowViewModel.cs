namespace DotnetBasics.API.Features.Bowling.Responses;

public record BowlingThrowViewModel
(
    Guid Id,
    int Pins,
    PlayerViewModel Player
);