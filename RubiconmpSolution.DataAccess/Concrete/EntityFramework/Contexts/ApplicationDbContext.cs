using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RubiconmpSolution.Core.Utilities.Constants;
using RubiconmpSolution.Core.Utilities.ThirdPartyLibraries.FakeData.Abstract;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.DataAccess.Concrete.EntityFramework.Contexts;

public class ApplicationDbContext : DbContext
{
    private readonly IDataFaker _dataFaker;
    
    public ApplicationDbContext(DbContextOptions options, IDataFaker dataFaker) : base(options)
    {
        _dataFaker = dataFaker;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.Load(AssemblyNames.DataAccess));

        Seed(builder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rectangle>().HasData(_dataFaker.GetFakeRectangleData(5000));
    }

    public DbSet<Rectangle> Rectangles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
}