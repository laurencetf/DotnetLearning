using DotnetBasics.API.Features.Bowling.Responses;
using MediatR;

namespace DotnetBasics.API.Bowling.Feature;

public class GetBowlingThrowsQuery : IRequest<BowlingThrowsPaginatedViewModel> {
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public DateTime? ThrowMinDate { get; set; }
    public DateTime? ThrowMaxDate { get; set; }
    public ThrowStatus? Status { get; set; }
}