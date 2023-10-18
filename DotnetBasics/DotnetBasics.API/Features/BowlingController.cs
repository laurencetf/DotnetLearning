using DotnetBasics.Application.Features.BowlingThrows.Commands;
using DotnetBasics.Application.Features.BowlingThrows.Models;
using DotnetBasics.Application.Features.BowlingThrows.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBasics.API.Bowling.Features;

[ApiController]
[Route("[controller]")]
public class BowlingController : ControllerBase
{
    
    // Service injectés 
    //Scope (public, internal, protected, private) modificateur (static) accessibilité (readonly, virtual)
    private readonly IMediator _mediator;

    //Paramètre injecté par le noyau dotnet après instruction dans le program.cs
    public BowlingController(IMediator mediator)
    {
        //Injection de dépendances 5ème principe SOLID (Dependancy Inversion)
        _mediator = mediator;
    }
    
    //Annotation corresponadant au verbe http 
    [HttpGet]
    //async toutes les fonctions asynchrone doivent avoir le modificateur async et retourner une Task
    public async Task<BowlingThrowList> GetBowlingThrows([FromQuery] GetBowlingThrowsQuery queryParams, CancellationToken cancellationToken)
    {
        try
        {
            //Implémentation
            //var type dynamic assigné à l'exécution
            var bowlingThrows = await _mediator.Send(queryParams, cancellationToken)
                //Facultatif sur les évènements asynchrones (permet d'éviter de créer un objet Awaitable) peut améliorer les perfs
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

    [HttpPost]
    public async Task<ActionResult> PostBowlingThrows([FromBody] PostBowlingThrowsCommand request, CancellationToken cancellationToken)
    {
        try {
            var bowlingThrow = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Created("", bowlingThrow);
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}