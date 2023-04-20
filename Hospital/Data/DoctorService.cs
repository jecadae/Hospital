using Hospital.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;

public class DoctorService
{
    
    
    private readonly AppDbContext _context;

    public DoctorService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получить всех докторов
    /// </summary>
    /// <returns>Массив докторов</returns>
    public async Task<Doctor[]> GetDoctorsAsync()
    {
        return await _context.Doctors.AsNoTracking().Where(x=>x.Status==false).ToArrayAsync();
    }
    /// <summary>
    /// Получить доктора по id
    /// </summary>
    /// <param name="id"> id доктора</param>
    /// <returns></returns>
    public async Task<Doctor> GetDoctorAsync(int id )
    {
        return await _context.Doctors.AsNoTracking().Where(x=>x.Status==false).FirstOrDefaultAsync(x=>x.Id==id );
    }
    
    /// <summary>
    /// Добавить доктора
    /// </summary>
    /// <param name="doctor">Поле доктора</param>
    /// <returns></returns>
    public async Task<bool> AddDoctorAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Обновить доктора
    /// </summary>
    /// <param name="id"> Id доктора которого меняем</param>
    /// <param name="doctor">новая модель доктора </param>
    /// <returns></returns>
    public async Task<bool> UpdateDoctorAsync(int id, Doctor doctor)
    {
        var result =await _context.Doctors.Where(x=>x.Status==false).FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
        {
            return false;
        }
        
        result.DoctorType = doctor.DoctorType;
        await _context.SaveChangesAsync();
        return true;

    }
    /// <summary>
    /// Удаление записи
    /// </summary>
    /// <param name="id">id удаляемого доктора </param>
    /// <returns>true если успех, false если доктор был не найден</returns>
    public async Task<bool> RemoveDoctorAsync(int id)
    {
        var result =await _context.Doctors.Where(x=>x.Status==false).FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
        {
            return false;
        }

        result.Status = true;
        //Ставим то что записи удалены
        await _context.Appointments.Where(x => x.DoctorId == result.Id).ExecuteUpdateAsync(s => s.SetProperty(t => t.Status, t => !t.Status ));
        //Удаляем расписание(Команду здесть надо поменять)
        //TODO
        await _context.Schedules.Where(x => x.DoctorId == result.Id).ExecuteUpdateAsync(s => s.SetProperty(t => t.Status, t => !t.Status ));
        await _context.SaveChangesAsync();
        return true;
    }
    
    
    





}