using DotnetBasics.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBasics.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    ) {
        return services.AddSingleton<BowlingRepository>();
    }
}
