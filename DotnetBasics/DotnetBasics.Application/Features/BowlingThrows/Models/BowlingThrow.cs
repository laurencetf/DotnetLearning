using DotnetBasics.Application.Features.BowlingThrows.Enums;

namespace DotnetBasics.Application.Features.BowlingThrows.Models;

public class BowlingThrow
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Pins { get; set; }
    public Player Player { get; set; }
    public ThrowStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
}