using UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Organizing.Domain.Services;

public interface IScheduleCommandService
{
    Task<Schedule> Handle(CreateScheduleCommand command);
    Task<Schedule?> Handle(UpdateScheduleCommand command);
    Task<bool> Handle(DeleteScheduleCommand command);
}