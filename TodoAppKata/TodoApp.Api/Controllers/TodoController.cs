using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{

    private readonly ILogger<TodoController> _logger;
    private readonly ISender _mediator;

    public TodoController(ILogger<TodoController> logger, ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetTodo")]
    public async Task<ActionResult> Get([FromQuery] TodoQuery todoQueryParam)
    {
        var result = await _mediator.Send(todoQueryParam);
        if (result?.Any() != true)
            return NotFound();
        return Ok(result);
    }

}
