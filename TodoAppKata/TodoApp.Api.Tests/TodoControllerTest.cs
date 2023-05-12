using Microsoft.Extensions.Logging;
using Moq;
using TodoApp.Api.Controllers;
using TodoApp.Api.ViewModels;
using Xunit;
using FluentAssertions;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using MediatR;

namespace TodoApp.Api.Tests;

public class TodoControllerTest
{

    private readonly Mock<ILogger<TodoController>> _loggerMock ;
    private readonly IFixture _fixture;

    private readonly Mock<ISender> _mediatorMock;
    public TodoControllerTest()
    {

        _loggerMock = new Mock<ILogger<TodoController>>();
        _mediatorMock = new Mock<ISender>();
        _fixture = new Fixture();

    }

    [Fact]
    public void WhenNoData_ShouldReturnNotFound()
    {
        //Arrange
        TodoQuery todoQueryParam = _fixture.Create<TodoQuery>();
        TodoController controller = new(_loggerMock.Object);
        //Act
        var result = controller.Get(todoQueryParam);
        //Assert
        (result as ObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public void WhenNoFilterAndDataProvided_ShouldReturnOkWithNotEmptyContent()
    {
        //Arrange
        TodoQuery todoQueryParam = new();
        TodoController controller = new(_loggerMock.Object);
        //Act
        var result = controller.Get(todoQueryParam);
        //Assert
        result.Should().NotBeNull();
        var objectResult = (result as ObjectResult)!;
        objectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (objectResult.Value as IEnumerable<TodoViewModel>).Should().NotBeNullOrEmpty();
    }
}
