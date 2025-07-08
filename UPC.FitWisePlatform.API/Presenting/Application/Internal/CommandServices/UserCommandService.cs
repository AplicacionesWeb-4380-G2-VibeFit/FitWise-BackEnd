using UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Presenting.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<User?> Handle(CreateUserCommand command)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email))
            throw new Exception("User with the same email already exists");
        
        if (await userRepository.ExistsByUsernameAsync(command.Username))
            throw new Exception("User with the same username already exists");
        var user = new User(
            command.FirstName, 
            command.LastName,
            command.Email, 
            command.BirthDate, 
            command.Gender, 
            command.Username,
            command.Password,
            command.Image,
            command.AboutMe);
        
        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();
        
        return user;
        
    }

    public async Task<User?> Handle(UpdateUserCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.Id);
        
        if (user == null)
            throw new Exception($"User with id '{command.Id}' does not exist");
        if(command.Username != user.Username && await userRepository.ExistsByUsernameAsync(command.Username))
            throw new Exception("User with the same username already exists");
        if(command.Email.EmailValue != user.Email.EmailValue && await userRepository.ExistsByEmailAsync(command.Email))
            throw new Exception("User with the same email already exists");
        
        user.UpdateDetails(
            command.FirstName, 
            command.LastName, 
            command.Email, 
            command.BirthDate, 
            command.Gender,
            command.Username,
            command.Password,
            command.Image,
            command.AboutMe);
        userRepository.Update(user);
        await unitOfWork.CompleteAsync();
        
        return user;

    }

    public async Task<bool> Handle(DeleteUserCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.Id);
        if (user == null)
            throw new Exception($"User with id '{command.Id}' does not exist");
        
        userRepository.Remove(user);
        await unitOfWork.CompleteAsync();
        return true;
    }
}