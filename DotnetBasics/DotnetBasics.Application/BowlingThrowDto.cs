using DotnetBasics.API.Bowling.Feature;

namespace DotnetBasics.Application;

public class BowlingThrowDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Pins { get; set; }
    
    public PlayerDto Player { get; set; }
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }
}