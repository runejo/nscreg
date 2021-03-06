using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nscreg.Data.Core.EntityConfiguration;
using nscreg.Data.Entities;

namespace nscreg.Data.Configuration
{
    /// <summary>
    /// Activity category configuration class
    /// </summary>
    public class ActivityCategoryConfiguration : EntityTypeConfigurationBase<ActivityCategory>
    {
        /// <summary>
        /// Activity category Configuration Method
        /// </summary>
        public override void Configure(EntityTypeBuilder<ActivityCategory> builder)
        {
            builder.Property(v => v.Code)
                .IsRequired()
                .HasMaxLength(10);
            builder.HasIndex(v => v.Code)
                .IsUnique();
            builder.Property(v => v.Name)
                .IsRequired();
            builder.Property(v => v.Section)
                .HasMaxLength(10)
                .IsRequired();
        }
    }
}
