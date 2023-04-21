using Hospital.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;

public class AppointmentsService: IHospitalService<Appointment>
{
    private readonly AppDbContext _context;

    public AppointmentsService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _context.Appointments.AsNoTracking().Where(x => x.IsFired == false).ToListAsync();
    }
    
    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Appointments.AsNoTracking().Where(x => x.IsFired == false)
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    /// <summary>
    /// Создание записи
    /// </summary>
    /// <param name="appointment">Модель записи</param>
    /// <returns></returns>
    public async Task<bool> CreateAsync(Appointment appointment)
    {
        var schedule =
            await _context.Schedules.AsNoTracking()
                .FirstOrDefaultAsync(a => a.IsFired == false && a.DoctorId == appointment.DoctorId);
        if (schedule == null) return false;
        if (schedule.WeekDay.Contains(appointment.StartVisit.DayOfWeek) == false) return false;
        var result = await _context.Appointments.AsNoTracking().Where(a =>
                a.IsFired == false && a.DoctorId == appointment.DoctorId && a.StartVisit == appointment.StartVisit)
            .ToListAsync();
        if (result.Count == 0) return false;
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return true;
    }  
    
    public async Task<bool> UpdateAsync(int id,  Appointment appointment)
    {
        var result = await _context.Appointments.Where(x => x.IsFired == false).FirstOrDefaultAsync(x => x.Id == id);
        if (result == null) return false;
        
        result.Patient = appointment.Patient;
        result.DoctorId = appointment.DoctorId;
        result.EndVisit= appointment.EndVisit;
        result.StartVisit = appointment.StartVisit;
        result.InsuranceNumberId=appointment.InsuranceNumberId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _context.Appointments.Where(x => x.IsFired == false).FirstOrDefaultAsync(x => x.Id == id);
        if (result == null) return false;
        result.IsFired = true;
        await _context.SaveChangesAsync();
        return true;
    }




}