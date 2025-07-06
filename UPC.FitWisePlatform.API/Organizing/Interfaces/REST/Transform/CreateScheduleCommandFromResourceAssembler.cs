using UPC.FitWisePlatform.API.Organizing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Transform;

public static class CreateScheduleCommandFromResourceAssembler
{
    public static CreateScheduleCommand ToCommandFromResource(CreateScheduleResource resource)
    {
     return new CreateScheduleCommand(resource.UserId, resource.HealthPlanId, resource.Date);   
    }
}