using UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(CreateUserCommand command);
    Task<User?> Handle(UpdateUserCommand command);
    Task<bool> Handle(DeleteUserCommand command);
    
}