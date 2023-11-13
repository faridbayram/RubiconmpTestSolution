using Autofac;
using RubiconmpSolution.Business.Abstract;
using RubiconmpSolution.Business.Concrete;
using RubiconmpSolution.Core.DependencyResolvers;
using RubiconmpSolution.Core.Utilities.ThirdPartyLibraries.FakeData.Abstract;
using RubiconmpSolution.Core.Utilities.ThirdPartyLibraries.FakeData.Concrete;
using RubiconmpSolution.DataAccess.Abstract;
using RubiconmpSolution.DataAccess.Concrete.EntityFramework.Contexts;
using RubiconmpSolution.DataAccess.Concrete.EntityFramework.DALC;

namespace RubiconmpSolution.Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : CoreModule
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        
        builder.RegisterType<ApplicationDbContext>();
        
        builder.RegisterType<RectangleService>().As<IRectangleService>();
        builder.RegisterType<EfRectangleDal>().As<IRectangleDal>();
        
        builder.RegisterType<UserService>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();
        
        builder.RegisterType<AuthService>().As<IAuthService>();

        builder.RegisterType<BogusDataFaker>().As<IDataFaker>();
    }
}