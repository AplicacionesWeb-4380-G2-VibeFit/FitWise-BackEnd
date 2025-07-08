using UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUserQuery query);
    
}