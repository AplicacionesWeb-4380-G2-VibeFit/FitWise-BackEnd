using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Organizing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Organizing.Infrastructure.Persistence.EFC.Repositories;

public class ScheduleRepository(AppDbContext context) : BaseRepository<Schedule>(context), IScheduleRepository
{
    public async Task<Schedule?> FindByIdAsync(int id)
    {
        return await Context.Set<Schedule>().FindAsync(id);
    }

    public async Task<IEnumerable<Schedule>> ListAsync()
    {
        return await Context.Set<Schedule>().ToListAsync();
    }

    public async Task<IEnumerable<Schedule>> FindByScheduleIdAsync(int scheduleId)
    {
        return await Context.Set<Schedule>()
            .Where(s => s.Id == scheduleId)
            .ToListAsync();
    }
}