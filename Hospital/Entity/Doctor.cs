using Hospital.Enum;

namespace Hospital.Entity;

public class Doctor
{
    public int Id { get; set; }
    public DoctorsType DoctorType { get; set; }
    public int? ScheduleId { get; set; }
    public bool IsFired { get; set; }
}