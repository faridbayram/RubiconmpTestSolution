using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RubiconmpSolution.DataAccess.Concrete.EntityFramework.Configurations.Base;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.DataAccess.Concrete.EntityFramework.Configurations;

public class UserConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Users");
        
        builder.Property(x => x.Username).HasColumnType("nvarchar").HasMaxLength(20).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.PasswordSalt).IsRequired();
        
        builder.HasIndex(i => i.Username,"NCL_IX_Username").IsUnique();
    }
}