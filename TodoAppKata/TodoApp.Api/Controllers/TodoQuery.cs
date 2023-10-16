using MediatR;
using TodoApp.Api.ViewModels;

namespace TodoApp.Api.Controllers;

public class TodoQuery : IRequest<IEnumerable<TodoViewModel>>
{

    public string? Status;
}

public class TodoQueryHandler : IRequestHandler<TodoQuery, IEnumerable<TodoViewModel>>
{
    private readonly TodoProvider _provider;

    public async Task<IEnumerable<TodoViewModel>> Handle(TodoQuery request, CancellationToken cancellationToken)
    {
        var todos = await _provider.GetTodosAsync(request).ConfigureAwait(false);
        return todos;
    }
}

internal class TodoProvider
{
    internal async Task<IEnumerable<TodoViewModel>> GetTodosAsync(TodoQuery request) //TODO : implement repository
    {
        if (String.Compare(request.Status,"DATA",true)==0){
            return new List<TodoViewModel>(){
                new TodoViewModel()
            };
        }
        return new List<TodoViewModel>();
    }
}
