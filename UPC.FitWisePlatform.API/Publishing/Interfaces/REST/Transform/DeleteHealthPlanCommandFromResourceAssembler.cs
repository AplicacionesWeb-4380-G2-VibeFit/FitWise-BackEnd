using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class DeleteHealthPlanCommandFromResourceAssembler
{
    public static DeleteHealthPlanCommand ToCommandFromResource(int healthPlanId)
    {
        return new DeleteHealthPlanCommand(healthPlanId);
    }
}