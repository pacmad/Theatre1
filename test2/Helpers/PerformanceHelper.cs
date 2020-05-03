using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using test2.Models.Performances;
using test2.Services;

namespace test2.Helpers
{
    public class PerformanceHelper : IPerformanceHelper
    {
        private readonly IPerformanceService service;
        public PerformanceHelper(IPerformanceService service)
        {
            this.service = service;
        }

        public Task<List<Performance>> GetAll()
        {
            return service.GetAll();
        }

        public Task<Performance> GetPerformance(Guid id)
        {
            return service.GetPerformance(id);
        }

        public Task<List<Performance>> GetPerformances(int page, int count, DateTime? date, string name)
        {
            if (date.HasValue && !String.IsNullOrEmpty(name))
                return service.GetPerformances(page, count, date.Value, name);
            else if (date.HasValue)
                return service.GetPerformances(page, count, date.Value);
            else if (!String.IsNullOrEmpty(name))
                return service.GetPerformances(page, count, name);

            return service.GetPerformances(page, count);
        }

        public Task<Int32> GetTotal(DateTime? date, String name)
        {
            if (date.HasValue && !String.IsNullOrEmpty(name))
                return service.GetTotal(date.Value, name);
            else if (date.HasValue)
                return service.GetTotal(date.Value);
            else if (!String.IsNullOrEmpty(name))
                return service.GetTotal(name);

            return service.GetTotal();
        }

        public Task Book(string performanceId, string timeId, int count, string userId)
        {
            return service.Book(Guid.Parse(performanceId), Guid.Parse(timeId), count, Guid.Parse(userId));
        }

        public async Task<bool> Edit(Performance performance)
        {
            Int32 result = await service.Edit(performance);
            return result == 1;
        }
    }
}
