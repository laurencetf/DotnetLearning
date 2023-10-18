namespace DotnetBasics.Application.Features.BowlingThrows.Models;

public class BowlingThrowList
{
    public int PageSize { get; set; }
    
    public int PageIndex { get; set; }
    
    public IEnumerable<BowlingThrow> Items { get; set; }
}
