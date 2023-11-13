using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RubiconmpSolution.Entities.Concrete.Base;

namespace RubiconmpSolution.DataAccess.Concrete.EntityFramework.Configurations.Base;

public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity, new()
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        
        builder.Property(p => p.CreatedBy).HasColumnType("nvarchar").HasMaxLength(100);
        builder.Property(p => p.CreatedAt).HasColumnType("smalldatetime");
        builder.Property(p => p.ModifiedBy).HasColumnType("nvarchar").HasMaxLength(100);
        builder.Property(p => p.ModifiedAt).HasColumnType("smalldatetime");
        builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
    }
}