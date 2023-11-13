using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RubiconmpSolution.Core.Utilities.IoC;

namespace RubiconmpSolution.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, IEnumerable<ICoreModule> modules)
    {
        foreach (var module in modules)
            module.Load(services);

        return ServiceTool.Create(services);
    }
}