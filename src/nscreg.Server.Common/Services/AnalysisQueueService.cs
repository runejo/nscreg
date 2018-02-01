using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nscreg.Data;
using nscreg.Data.Constants;
using nscreg.Data.Entities;
using nscreg.Resources.Languages;
using nscreg.Server.Common.Models.AnalysisQueue;
using nscreg.Server.Common.Models.StatUnits.Edit;
using nscreg.Server.Common.Services.StatUnit;
using Newtonsoft.Json;

namespace nscreg.Server.Common.Services
{
    public class AnalysisQueueService
    {
        private readonly NSCRegDbContext _context;
        private readonly ViewService _viewSvc;
        private readonly EditService _editSvc;

        public AnalysisQueueService(NSCRegDbContext context, ViewService viewSvc, EditService editSvc)
        {
            _context = context;
            _viewSvc = viewSvc;
            _editSvc = editSvc;
        }

        public async Task<AnalysisQueueListModel> GetAsync(SearchQueryModel filter)
        {
            IQueryable<AnalysisQueue> query = _context.AnalysisQueues.Include(x => x.User);

            if (filter.DateFrom.HasValue)
                query = query.Where(x => x.UserStartPeriod >= filter.DateFrom.Value);

            if (filter.DateTo.HasValue)
                query = query.Where(x => x.UserEndPeriod <= filter.DateTo.Value);

            var total = query.Count();
            var result = await query
                .Skip(filter.PageSize * (filter.Page - 1))
                .Take(filter.PageSize)
                .ToListAsync();

            return new AnalysisQueueListModel
            {
                TotalCount = total,
                CurrentPage = filter.Page,
                Items = Mapper.Map<IList<AnalysisQueueModel>>(result ?? new List<AnalysisQueue>()),
                PageSize = filter.PageSize,
                TotalPages = (int) Math.Ceiling((double) total / filter.PageSize)
            };
        }

        public async Task<AnalysisQueue> CreateAsync(AnalisysQueueCreateModel data, string userId)
        {
            var domain = Mapper.Map<AnalysisQueue>(data);
            domain.UserId = userId;
            _context.AnalysisQueues.Add(domain);
            await _context.SaveChangesAsync();
            return domain;
        }

        public async Task<LogItemsListModel> GetLogs(LogsQueryModel filter)
        {
            var logs = _context.AnalysisLogs
                .Where(x => x.AnalysisQueueId == filter.QueueId)
                .OrderBy(x => x.Id);

            var total = await logs.CountAsync();

            var paginated = await logs
                .Skip(filter.PageSize * (filter.Page - 1))
                .Take(filter.PageSize)
                .ToListAsync();

            var suIds = paginated.Where(x => x.AnalyzedUnitType != StatUnitTypes.EnterpriseGroup)
                .Select(x => x.AnalyzedUnitId);
            var egIds = paginated.Where(x => x.AnalyzedUnitType == StatUnitTypes.EnterpriseGroup)
                .Select(x => x.AnalyzedUnitId);

            var names = (await _context.StatisticalUnits
                    .Where(su => suIds.Contains(su.RegId))
                    .ToDictionaryAsync(x => (x.RegId, x.UnitType), x => x.Name))
                .Concat(await _context.EnterpriseGroups
                    .Where(eg => egIds.Contains(eg.RegId))
                    .ToDictionaryAsync(x => (x.RegId, StatUnitTypes.EnterpriseGroup), x => x.Name))
                .ToDictionary(x => x.Key, x => x.Value);

            var result = paginated.Select(x => new LogItemModel
            {
                Id = x.Id,
                SummaryMessages = x.SummaryMessages.Split(';'),
                UnitId = x.AnalyzedUnitId,
                UnitName = names[(x.AnalyzedUnitId, x.AnalyzedUnitType)],
                UnitType = x.AnalyzedUnitType.ToString(),
                IssuedAt = x.IssuedAt,
                ResolvedAt = x.ResolvedAt,
            }).ToList();

            return new LogItemsListModel
            {
                TotalCount = total,
                CurrentPage = filter.Page,
                Items = result,
                PageSize = filter.PageSize,
                TotalPages = (int) Math.Ceiling((double) total / filter.PageSize)
            };
        }

        public async Task<LogItemDetailsVm> GetLogEntry(int id)
        {
            var entity = await _context.AnalysisLogs
                .Include(x => x.AnalysisQueue)
                .FirstOrDefaultAsync(x => x.Id == id && x.ResolvedAt == null);

            if (entity == null) throw new NotFoundException(nameof(Resource.AnalysisLogEntryNotFound));

            var metadata = await _viewSvc.GetViewModel(
                entity.AnalyzedUnitId,
                entity.AnalyzedUnitType,
                entity.AnalysisQueue.UserId);

            return LogItemDetailsVm.Create(
                entity,
                metadata.Properties,
                metadata.Permissions);
        }

        public async Task<Dictionary<string, string[]>> UpdateLogEntry(int logId, string data, string userId)
        {
            var logEntry = await _context.AnalysisLogs
                .Include(x => x.AnalysisQueue)
                .FirstOrDefaultAsync(x => x.Id == logId);

            if (logEntry == null) throw new NotFoundException(nameof(Resource.AnalysisLogEntryNotFound));

            Task<Dictionary<string, string[]>> updateUnitTask;
            switch (logEntry.AnalyzedUnitType)
            {
                case StatUnitTypes.LocalUnit:
                    updateUnitTask = _editSvc.EditLocalUnit(await PopulateModel<LocalUnitEditM>(_context.LocalUnits),
                        userId);
                    break;
                case StatUnitTypes.LegalUnit:
                    updateUnitTask = _editSvc.EditLegalUnit(await PopulateModel<LegalUnitEditM>(_context.LegalUnits),
                        userId);
                    break;
                case StatUnitTypes.EnterpriseUnit:
                    updateUnitTask =
                        _editSvc.EditEnterpriseUnit(await PopulateModel<EnterpriseUnitEditM>(_context.EnterpriseUnits),
                            userId);
                    break;
                case StatUnitTypes.EnterpriseGroup:
                    updateUnitTask =
                        _editSvc.EditEnterpriseGroup(
                            await PopulateModel<EnterpriseGroupEditM>(_context.EnterpriseGroups), userId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Parameter `{nameof(data)}`: value of type `{logEntry.AnalyzedUnitType.ToString()}` is not supported.");
            }

            var errors = await updateUnitTask;
            if (errors?.Any() == true) return errors;

            logEntry.ResolvedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return null;

            async Task<TModel> PopulateModel<TModel>(IQueryable<IStatisticalUnit> source)
            {
                var existingEntity = await source.FirstOrDefaultAsync(x => x.RegId == logEntry.AnalyzedUnitId);
                var existingModel = Mapper.Map<TModel>(existingEntity);
                JsonConvert.PopulateObject(data, existingModel);
                return existingModel;
            }
        }
    }
}