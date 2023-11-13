using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RubiconmpSolution.DataAccess.Concrete.EntityFramework.Configurations.Base;
using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.DataAccess.Concrete.EntityFramework.Configurations;

public class RectangleConfiguration : BaseEntityTypeConfiguration<Rectangle>
{
    public override void Configure(EntityTypeBuilder<Rectangle> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Rectangles");
        
        builder.HasIndex(i => new
        {
            i.TopLeftCoordinateX,
            i.TopLeftCoordinateY, 
            i.BottomRightCoordinateX, 
            i.BottomRightCoordinateY
        }, "NCL_IX_TopLeftAndBottomRightCoordinates");
    }
}