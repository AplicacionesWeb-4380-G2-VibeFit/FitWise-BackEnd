﻿using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        if (!Enum.TryParse(resource.Gender, true, out Gender gender))
        {
            throw new ArgumentException($"Invalid Gender value: {resource.Gender}");
        }
        
        return new CreateUserCommand(
            resource.FirstName,
            resource.LastName,
            new Email(resource.Email),
            new BirthDate(resource.BirthDate),
            gender,
            new Image(resource.Image),
            resource.AboutMe,
            resource.ProfileId);
    }
}