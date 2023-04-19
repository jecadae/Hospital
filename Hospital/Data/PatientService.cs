using Hospital.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;

public class PatientService
{
    private readonly AppDbContext _context;

    public PatientService(AppDbContext context)
    {
        _context = context;
    }
    

    public async Task<Patient[]> GetPatientsAsync()
    {
        return await _context.Patients.AsNoTracking().Where(x=>x.Status==false).ToArrayAsync();
    }

    public async Task<Patient> GetPatientAsync(long polis )
    {
        return await _context.Patients.AsNoTracking().Where(x=>x.Status==false).FirstOrDefaultAsync(x=>x.Polis==polis );
    }
    

    public async Task<bool> AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdatePatientAsync(long polis, Patient patient)
    {
        var result =await _context.Patients.Where(x=>x.Status==false).FirstOrDefaultAsync(x => x.Polis ==polis);
        if (result == null)
        {
            return false;
        }

        result.Age = patient.Age;
        result.Name = patient.Name;
        await _context.SaveChangesAsync();
        return true;

    }
    
    
    public async Task<bool> RemovePatientAsync(long polis )
    {
        var result = _context.Patients.Where(x=>x.Status==false).FirstOrDefaultAsync(x => x.Polis == polis);
        if (result.Result == null)
        {
            return false;
        }

        result.Result.Status = true;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Получить все записи пользователя, в том числе все что  были или не состоятся
    /// </summary>
    /// <param name="polis">Полис пользователя</param>
    /// <returns>Лист записей</returns>
    public async Task<List<Appointment>> GetAllPatientAppointmentAsync(long polis)
    {
        return await _context.Appointments.AsNoTracking().Where(x=>(x.PatientPolis==polis)||(x.Status==false)).ToListAsync();
    }

    
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="polis"></param>
    /// <returns></returns>
    public async Task<List<Appointment>> GetAllPatientAppointmentToVisitAsync(long polis)
    {
        return await _context.Appointments.AsNoTracking().Where(x=>(x.PatientPolis==polis)||(x.Status==false)||(x.StartVisit.Date>DateTime.Now)).ToListAsync();
    }

}