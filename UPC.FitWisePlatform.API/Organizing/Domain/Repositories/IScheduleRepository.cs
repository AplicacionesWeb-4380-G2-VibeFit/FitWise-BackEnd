using UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Organizing.Domain.Repositories;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    Task<IEnumerable<Schedule>> FindByScheduleIdAsync(int scheduleId);
}