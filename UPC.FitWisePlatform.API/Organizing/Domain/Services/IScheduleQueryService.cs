using UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Organizing.Domain.Services;

public interface IScheduleQueryService
{
    Task<IEnumerable<Schedule>> Handle(GetAllSchedulesQuery query);
    Task<Schedule?> Handle(GetScheduleByIdQuery query);
}