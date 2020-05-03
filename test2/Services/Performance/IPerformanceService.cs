using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using test2.Models.Performances;

namespace test2.Services
{
    public interface IPerformanceService
    {
        Task<List<Performance>> GetPerformances(Int32 page, Int32 count, DateTime date, String name);
        Task<List<Performance>> GetPerformances(Int32 page, Int32 count, DateTime date);
        Task<List<Performance>> GetPerformances(Int32 page, Int32 count);
        Task<List<Performance>> GetPerformances(Int32 page, Int32 count, String name);
        Task<Performance> GetPerformance(Guid id);
        Task<List<Performance>> GetAll();
        Task<Int32> GetTotal();
        Task<Int32> GetTotal(DateTime date, String name);
        Task<Int32> GetTotal(String name);
        Task<Int32> GetTotal(DateTime date);
        Task Book(Guid performanceId, Guid timeId, Int32 count, Guid userId);
        Task<Int32> Edit(Performance performance);
    }
}
