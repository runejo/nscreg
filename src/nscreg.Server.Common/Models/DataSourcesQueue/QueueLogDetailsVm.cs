using System;
using nscreg.Data.Constants;
using nscreg.Data.Entities;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace nscreg.Server.Common.Models.DataSourcesQueue
{
    /// <summary>
    /// Вью модель подробной очереди журнала  
    /// </summary>
    public class QueueLogDetailsVm
    {
        private QueueLogDetailsVm(DataUploadingLog item)
        {
            Id = item.Id;
            Started = item.StartImportDate;
            Ended = item.EndImportDate;
            StatId = item.TargetStatId;
            Name = item.StatUnitName;
            Unit = item.SerializedUnit;
            Status = item.Status;
            Note = item.Note;
        }

        /// <summary>
        /// Метод создания вью модели подробной очереди журнала 
        /// </summary>
        /// <param name="item">Единица</param>
        /// <returns></returns>
        public static QueueLogDetailsVm Create(DataUploadingLog item) => new QueueLogDetailsVm(item);

        public int Id { get; }
        public DateTime? Started { get; }
        public DateTime? Ended { get; }
        public string StatId { get; }
        public string Name { get; }
        public string Unit { get; }
        public DataUploadingLogStatuses Status { get; }
        public string Note { get; }
    }
}
