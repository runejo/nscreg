using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nscreg.Data;
using nscreg.Data.Entities;
using nscreg.Server.Common.Models.Addresses;
using nscreg.Server.Common.Models.AnalysisQueue;

namespace nscreg.Server.Common.Services
{
    public class AnalysisQueueService
    {
        private readonly NSCRegDbContext _context;

        public AnalysisQueueService(NSCRegDbContext context)
        {
            _context = context;
        }

        public async Task<AnalysisQueueListModel> GetAsync(int page, int pageSize)
        {
            IQueryable<AnalysisQueue> query = _context.AnalysisQueues.Include(x=>x.User);
            
            var total = query.Count();
            var result = await query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

            return new AnalysisQueueListModel
            {
                TotalCount = total,
                CurrentPage = page,
                Items = Mapper.Map<IList<AnalysisQueueModel>>(result ?? new List<AnalysisQueue>()),
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)(total) / pageSize)
            };
        }
    }
}
