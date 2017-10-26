using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nscreg.Data.Core.EntityConfiguration;
using nscreg.Data.Entities;

namespace nscreg.Data.Configuration
{
    /// <inheritdoc />
    /// <summary>
    ///  Класс конфигурации журнала анализа
    /// </summary>
    public class AnalysisLogConfiguration : EntityTypeConfigurationBase<AnalysisLog>
    {
        /// <inheritdoc />
        /// <summary>
        ///  Метод конфигурации журнала анализа
        /// </summary>
        public override void Configure(EntityTypeBuilder<AnalysisLog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.AnalysisQueue).WithMany(x => x.AnalysisLogs).HasForeignKey(x => x.AnalysisQueueId);
        }
    }
}
