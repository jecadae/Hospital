using Hospital.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;

public class ScheduleService: IHospitalService<Schedule>
{
     private readonly AppDbContext _context;

    public ScheduleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Schedule>> GetAllAsync()
    {
        return await _context.Schedules.AsNoTracking().Where(x => x.IsFired == false).ToListAsync();
    }


    public async Task<Schedule?> GetByIdAsync(int id)
    {
        return await _context.Schedules.AsNoTracking().Where(x => x.IsFired == false)
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> CreateAsync(Schedule schedule)
    {
        await _context.Schedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> UpdateAsync(int id, Schedule schedule)
    {
        var result = await _context.Schedules.Where(x => x.IsFired == false).FirstOrDefaultAsync(x => x.Id == id);
        if (result == null) 
            return false;
        
        result.BeginDay = schedule.BeginDay;
        result.BeginLunch = schedule.BeginLunch;
        result.EndDay = schedule.EndLunch;
        result.EndLunch = schedule.EndLunch;
        result.ReceptionInterval = schedule.ReceptionInterval;
        result.WeekDay = schedule.WeekDay;
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _context.Schedules.Where(x => x.IsFired == false).FirstOrDefaultAsync(x => x.Id == id);
        if (result == null) 
            return false;
        result.IsFired = true;
        await _context.SaveChangesAsync();
        return true;
    }
}