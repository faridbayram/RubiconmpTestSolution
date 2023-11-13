using Autofac;
using Microsoft.Extensions.DependencyInjection;
using RubiconmpSolution.Core.Utilities.IoC;
using RubiconmpSolution.Core.Utilities.Security.JWT;

namespace RubiconmpSolution.Core.DependencyResolvers;

public class CoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<JwtHelper>().As<ITokenHelper>().InstancePerDependency();
    }
}