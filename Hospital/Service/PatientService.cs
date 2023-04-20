using Hospital.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;

public class PatientService : IHospitalService<Patient>
{
    private readonly AppDbContext _context;

    public PatientService(AppDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Получить всех пациентов
    /// </summary>
    /// <returns>Лист пациентов</returns>
    public async Task<List<Patient>> GetAllAsync()
    {
        return await _context.Patients.AsNoTracking().Where(x => x.IsFired == false).ToListAsync();
    }
    /// <summary>
    /// Получить пациента по id
    /// </summary>
    /// <param name="InsuranceNumberId">id пациента</param>
    /// <returns></returns>
    public async Task<Patient> GetByIdAsync(int InsuranceNumberId)
    {
        return await _context.Patients.AsNoTracking().Where(x => x.IsFired == false)
            .FirstOrDefaultAsync(x => x.InsuranceNumberId == InsuranceNumberId);
    }

    /// <summary>
    /// Создание пациента
    /// </summary>
    /// <param name="patient">модель пациента</param>
    /// <returns> </returns>
    public async Task<bool> CreateAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return true;
    }
    /// <summary>
    /// Обновить пациента
    /// </summary>
    /// <param name="InsuranceNumberId">Id пациента</param>
    /// <param name="patient">модель пациента</param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(int InsuranceNumberId, Patient patient)
    {
        var result = await _context.Patients.Where(x => x.IsFired == false)
            .FirstOrDefaultAsync(x => x.InsuranceNumberId == InsuranceNumberId);
        if (result == null) return false;

        result.Age = patient.Age;
        result.Name = patient.Name;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удалить пациента
    /// </summary>
    /// <param name="InsuranceNumberId">id пациента</param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(int InsuranceNumberId)
    {
        var result = _context.Patients.Where(x => x.IsFired == false)
            .FirstOrDefaultAsync(x => x.InsuranceNumberId == InsuranceNumberId);
        if (result.Result == null) return false;

        result.Result.IsFired = true;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    ///     Получить все записи пользователя, в том числе все что  были или не состоятся
    /// </summary>
    /// <param name="InsuranceNumberId">Полис пользователя</param>
    /// <returns>Лист записей</returns>
    public async Task<List<Appointment>> GetAllPatientAppointmentAsync(int InsuranceNumberId)
    {
        return await _context.Appointments.AsNoTracking()
            .Where(x => x.InsuranceNumberId == InsuranceNumberId || x.IsFired == false)
            .ToListAsync();
    }


    /// <summary>
    ///     Так как при удалении врача все записи тоже обнуляются мы достаем только те что не удалены и те чье время больше
    ///     текущего
    /// </summary>
    /// <param name="InsuranceNumberId"></param>
    /// <returns></returns>
    public async Task<List<Appointment>> GetAllPatientAppointmentToVisitAsync(int InsuranceNumberId)
    {
        return await _context.Appointments.AsNoTracking().Where(x =>
            x.InsuranceNumberId == InsuranceNumberId || x.IsFired == false ||
            x.StartVisit.Date > DateTimeProvider.UtcNow).ToListAsync();
    }
    /// <summary>
    /// Создание записи
    /// </summary>
    /// <param name="appointment">Модель записи</param>
    /// <returns></returns>
    public async Task<bool> CreateAppointmentAsync(Appointment appointment)
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

    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <param name="id">id записей</param>
    /// <returns></returns>
    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var result = await _context.Appointments.FirstOrDefaultAsync(x=>x.Id==id);
        if (result == null) return false;
        result.IsFired = true;
        await _context.SaveChangesAsync();
        return true;
    }
}