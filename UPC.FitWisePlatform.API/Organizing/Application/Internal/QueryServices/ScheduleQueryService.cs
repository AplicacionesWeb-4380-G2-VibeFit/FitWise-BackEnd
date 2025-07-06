using UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Organizing.Domain.Repositories;
using UPC.FitWisePlatform.API.Organizing.Domain.Services;

namespace UPC.FitWisePlatform.API.Organizing.Application.Internal.QueryServices;

public class ScheduleQueryService(IScheduleRepository scheduleRepository) : IScheduleQueryService
{
    public async Task<IEnumerable<Schedule>> Handle(GetAllSchedulesQuery query)
    {
        return await scheduleRepository.ListAsync();
    }

    public async Task<Schedule?> Handle(GetScheduleByIdQuery query)
    {
        return await scheduleRepository.FindByIdAsync(query.Id);
    }
}