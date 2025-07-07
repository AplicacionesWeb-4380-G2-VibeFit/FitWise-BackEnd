using UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Transform;

public static class ScheduleResourceFromEntityAssembler
{
    public static ScheduleResource ToResourceFromEntity(Schedule entity)
    {
        return new ScheduleResource(
            entity.Id,
            entity.UserId,
            entity.HealthPlanId,
            entity.Date
            );
    }
    
}