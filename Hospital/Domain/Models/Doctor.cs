using Hospital.Enum;

namespace Hospital.Entity;
/// <summary>
/// Модель доктора
/// </summary>
public class Doctor
{
    public int Id { get; set; }
    public DoctorsType DoctorType { get; set; }
    public bool IsFired { get; set; }
    public int? ScheduleId { get; set; }
    public List<Appointment> Appointments { get; set; }
}