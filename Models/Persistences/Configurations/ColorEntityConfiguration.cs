using BigonWebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigonWebUI.Models.Persistences.Configurations
{
    public class ColorEntityConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.Id);  

            builder.Property(x => x.Id).HasColumnType("int");

            builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(20).IsRequired();

            builder.Property(x => x.HexCode).HasColumnType("nvarchar").HasMaxLength(7).IsRequired();

            builder.ConfigureAsAuditable();
           
            builder.ToTable("Colors");
        }
      
    }


}
