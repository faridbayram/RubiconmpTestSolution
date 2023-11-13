using Microsoft.Extensions.DependencyInjection;

namespace RubiconmpSolution.Core.Utilities.IoC;

public interface ICoreModule
{
    void Load(IServiceCollection collection);
}