﻿using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class UpdateHealthPlanExerciseCommandFromResourceAssembler
{
    public static UpdateHealthPlanExerciseCommand ToCommandFromResource(
        int id, UpdateHealthPlanExerciseResource resource)
    {
        if (!Enum.TryParse(resource.DayOfWeek, true, out DayOfWeekType dayOfWeekType))
        {
            throw new ArgumentException($"Invalid DayOfWeek value: {resource.DayOfWeek}");
        }
        
        return new UpdateHealthPlanExerciseCommand(id, resource.HealthPlanId, resource.ExerciseId, dayOfWeekType,
            resource.Instructions, resource.Sets, resource.Reps, resource.DurationInMinutes);
    }
}