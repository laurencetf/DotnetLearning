using DotnetBasics.Application.Features.BowlingThrows.Enums;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using MediatR;

namespace DotnetBasics.Application.Features.BowlingThrows.Queries;

public class GetBowlingThrowsQuery : IRequest<BowlingThrowList> {
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public DateTime? ThrowMinDate { get; set; }
    public DateTime? ThrowMaxDate { get; set; }
    public ThrowStatus? Status { get; set; }
}