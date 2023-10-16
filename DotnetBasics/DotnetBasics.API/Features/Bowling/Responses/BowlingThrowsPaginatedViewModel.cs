namespace DotnetBasics.API.Features.Bowling.Responses;

public class BowlingThrowsPaginatedViewModel
{
    public int PageSize { get; set; }
    
    public int PageIndex { get; set; }
    
    public IEnumerable<BowlingThrowViewModel> Items { get; set; }
}