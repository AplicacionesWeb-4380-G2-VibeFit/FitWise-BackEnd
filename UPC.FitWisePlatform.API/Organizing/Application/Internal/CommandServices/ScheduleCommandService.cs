using UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Organizing.Domain.Repositories;
using UPC.FitWisePlatform.API.Organizing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;


namespace UPC.FitWisePlatform.API.Organizing.Application.Internal.CommandServices;

public class ScheduleCommandService(IScheduleRepository scheduleRepository, IUnitOfWork unitOfWork) : IScheduleCommandService
{
    public async Task<Schedule> Handle(CreateScheduleCommand command)
    {
        var schedule = new Schedule(command.UserId, command.HealthPlanId, command.Date);
        await scheduleRepository.AddAsync(schedule);
        await unitOfWork.CompleteAsync();
        return schedule;
    }

    public async Task<Schedule?> Handle(UpdateScheduleCommand command)
    {
        var schedule = await scheduleRepository.FindByIdAsync(command.Id);
        if (schedule == null) return null;
        
        schedule.Update(command.Date);
        scheduleRepository.Update(schedule);
        await unitOfWork.CompleteAsync();
        return schedule;
    }

    public async Task<bool> Handle(DeleteScheduleCommand command)
    {
        var schedule = await scheduleRepository.FindByIdAsync(command.Id);
        if (schedule == null) return false;
        
        scheduleRepository.Remove(schedule);
        await unitOfWork.CompleteAsync();
        return true;
    }
}