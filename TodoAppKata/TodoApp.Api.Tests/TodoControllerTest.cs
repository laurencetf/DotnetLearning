using Microsoft.Extensions.Logging;
using Moq;
using TodoApp.Api.Controllers;
using Xunit;

namespace TodoApp.Api.Tests;

public class TodoControllerTest
{
    
    public Mock<ILogger<TodoController>> LoggerMock { get; set; }

    public TodoControllerTest()
    {
        LoggerMock = new Mock<ILogger<TodoController>>();
    }
    
    [Fact]
    public void WhenNoData_ShouldReturnEmptyOrNull()
    {
        var result = new TodoController(LoggerMock.Object);
    }
}