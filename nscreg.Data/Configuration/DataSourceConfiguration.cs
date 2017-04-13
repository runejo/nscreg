﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nscreg.Data.Entities;
using nscreg.Data.Infrastructure.EntityConfiguration;

namespace nscreg.Data.Configuration
{
    public class DataSourceConfiguration : EntityTypeConfigurationBase<DataSource>
    {
        public override void Configure(EntityTypeBuilder<DataSource> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.DataSources).HasForeignKey(x => x.UserId);
        }
    }
}
