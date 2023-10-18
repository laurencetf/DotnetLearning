using DotnetBasics.Application.Features.BowlingThrows.Abstraction.Interfaces;
using DotnetBasics.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBasics.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services
    ) {
        services.AddScoped<IBowlingThrowsRepository, BowlingRepository>();
        
        return services;
    }
}
