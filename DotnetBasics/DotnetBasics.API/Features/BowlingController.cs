using DotnetBasics.API.Features.Bowling.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBasics.API.Bowling.Feature;

[ApiController]
[Route("[controller]")]
public class BowlingController : ControllerBase
{
    
    // Service injectés 
    //Scope (public, internal, protected, private) modificateur (static) accessibilité (readonly, virtual)
    private readonly IMediator _mediator;

    //Paramètre injecté par le noyau dotnet après instructiond dans le program.cs
    public BowlingController(IMediator mediator)
    {
        //Injuection de dépendances 5ème principe SOLID (Dependancy Inversion)
        _mediator = mediator;
    }
    
    //Annotation corresponadant au verbe http 
    [HttpGet]
    //async toutes les fonctions asynchrone doivent avoir le modificateur async et retourner une Task
    public async Task<BowlingThrowsPaginatedViewModel> GetBowlingThrows([FromQuery] GetBowlingThrowsQuery queryParams, CancellationToken cancellationToken)
    {
        try
        {
            //Implémentation
            //var type dynamic assigné à l'exécution
            var bowlingThrows = await _mediator.Send(queryParams, cancellationToken)
                //Facultatif sur les évènements asynchrones (permet d'éviter de crééer un objet Awaitable) peut améliorer les perfs
                .ConfigureAwait(false);
            
            return bowlingThrows;
        }
        //Exception c'est la classe mère de toutes les Exceptions
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}