namespace DotnetBasics.Application.Features.BowlingThrows.Models;

public record Player
(
    string FirstName,
    string LastName
)
{
    public static Player FromDto(PlayerDto player)
    {
        return new Player
        (
            player.FirstName,
            player.LastName
        );
    }
}