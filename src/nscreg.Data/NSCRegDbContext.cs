using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using nscreg.Data.Core;
using nscreg.Data.Entities;
using nscreg.Data.Entities.History;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace nscreg.Data
{
    /// <summary>
    /// Контекст данных приложения
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class NSCRegDbContext : IdentityDbContext<User, Role, string,IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public NSCRegDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Метод обработчик создания модели
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.AddEntityTypeConfigurations(GetType().GetTypeInfo().Assembly);
            builder.Entity<User>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            builder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });
            builder.Entity<LegalUnit>().HasBaseType<StatisticalUnit>();
            builder.Entity<EnterpriseUnit>().HasBaseType<StatisticalUnit>();
            builder.Entity<LocalUnit>().HasBaseType<StatisticalUnit>();

        }
        public DbSet<StatisticalUnit> StatisticalUnits { get; set; }
        public DbSet<LegalUnit> LegalUnits { get; set; }
        public DbSet<EnterpriseUnit> EnterpriseUnits { get; set; }
        public DbSet<LocalUnit> LocalUnits { get; set; }
        public DbSet<EnterpriseGroup> EnterpriseGroups { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityStatisticalUnit> ActivityStatisticalUnits { get; set; }
        public DbSet<ActivityCategory> ActivityCategories { get; set; }
        public DbSet<ActivityCategoryUser> ActivityCategoryUsers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataSourceQueue> DataSourceQueues { get; set; }
        public DbSet<DataUploadingLog> DataUploadingLogs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LegalForm> LegalForms { get; set; }
        public DbSet<SectorCode> SectorCodes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonStatisticalUnit> PersonStatisticalUnits { get; set; }
        public DbSet<UserRegion> UserRegions { get; set; }
        public DbSet<AnalysisLog> AnalysisLogs { get; set; }
        public DbSet<SampleFrame> SampleFrames { get; set; }
        public DbSet<ReorgType> ReorgTypes { get; set; }
        public DbSet<ForeignParticipation> ForeignParticipations { get; set; }
        public DbSet<DataSourceClassification> DataSourceClassifications { get; set; }
        public DbSet<UnitStatus> Statuses { get; set; }
        public DbSet<UnitSize> UnitsSize { get; set; }
        public DbSet<CountryStatisticalUnit> CountryStatisticalUnits { get; set; }
        public DbSet<PostalIndex> PostalIndices { get; set; }
        public DbSet<DictionaryVersion> DictionaryVersions { get; set; }
        public DbSet<AnalysisQueue> AnalysisQueues { get; set; }
        public virtual DbSet<StatUnitSearchView> StatUnitSearchView { get; set; }
        public DbSet<ReportTree> ReportTree { get; set; }
        public DbSet<RegistrationReason> RegistrationReasons { get; set; }
        public DbSet<CustomAnalysisCheck> CustomAnalysisChecks { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }

        #region History
        public DbSet<StatisticalUnitHistory> StatisticalUnitHistory { get; set; }
        public DbSet<LocalUnitHistory> LocalUnitHistory { get; set; }
        public DbSet<LegalUnitHistory> LegalUnitHistory { get; set; }
        public DbSet<EnterpriseUnitHistory> EnterpriseUnitHistory { get; set; }
        public DbSet<EnterpriseGroupHistory> EnterpriseGroupHistory { get; set; }
        public DbSet<ActivityStatisticalUnitHistory> ActivityStatisticalUnitHistory { get; set; }
        public DbSet<CountryStatisticalUnitHistory> CountryStatisticalUnitHistory { get; set; }
        public DbSet<PersonStatisticalUnitHistory> PersonStatisticalUnitHistory { get; set; }
        #endregion
    }
}
