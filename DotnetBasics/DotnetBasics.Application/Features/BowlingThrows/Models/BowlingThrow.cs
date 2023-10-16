namespace DotnetBasics.Application.Features.BowlingThrows.Models;

public record BowlingThrow(
    Guid Id,
    int Pins,
    Player Player
)
{
    public static BowlingThrow FromDto(BowlingThrowDto bowlingThrow)
    {
        return new BowlingThrow(
            bowlingThrow.Id,
            bowlingThrow.Pins,
            Player.FromDto(bowlingThrow.Player)
        );

    }
}