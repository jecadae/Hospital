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
        var result = await _context.Patients.Where(x => x.IsFired == false)
            .FirstOrDefaultAsync(x => x.InsuranceNumberId == InsuranceNumberId);
        if (result == null) return false;
        result.IsFired = true;
        await _context.Appointments.Where(x => x.InsuranceNumberId == result.InsuranceNumberId).ExecuteUpdateAsync(s => s.SetProperty(t => t.IsFired, t => !t.IsFired));
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



}