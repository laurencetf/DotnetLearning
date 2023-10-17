namespace DotnetBasics.API.Features.Bowling.Requests.Enum;

// Injecter dans le program.cs un converter StringToEnum pour serialiser implicitement en json les enums 
public enum ThrowStatus
{
    Thrown = 0,
    Waiting = 1,
    InProgress = 2,
    Cancelled = 3
}