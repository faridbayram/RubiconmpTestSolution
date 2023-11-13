using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RubiconmpSolution.DataAccess.Concrete.EntityFramework.Configurations.Base;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.DataAccess.Concrete.EntityFramework.Configurations;

public class OperationClaimConfiguration : BaseEntityTypeConfiguration<OperationClaim>
{
    public override void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("OperationClaims");
        
        builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();

        builder.HasIndex(i => i.Name, "NCL_IX_Name").IsUnique();
    }
}