using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nscreg.Data.Core.EntityConfiguration;
using nscreg.Data.Entities;

namespace nscreg.Data.Configuration
{
    /// <summary>
    ///  Person configuration class stat. units
    /// </summary>
    public class PersonStatisticalUnitConfiguration : EntityTypeConfigurationBase<PersonStatisticalUnit>
    {
        public override void Configure(EntityTypeBuilder<PersonStatisticalUnit> builder)
        {
            builder.HasKey(v => new { v.UnitId, v.PersonId});
            builder.HasOne(v => v.Person).WithMany(v => v.PersonsUnits).HasForeignKey(v => v.PersonId);
            builder.HasOne(v => v.Unit).WithMany(v => v.PersonsUnits).HasForeignKey(v => v.UnitId);
          
            builder.Property(p => p.PersonId).HasColumnName("Person_Id");
            builder.Property(p => p.UnitId).HasColumnName("Unit_Id");
            builder.Property(p => p.EnterpriseGroupId).HasColumnName("GroupUnit_Id");

            builder.HasIndex(x => new { x.PersonTypeId, x.UnitId, x.PersonId}).IsUnique();
        }
    }
}
