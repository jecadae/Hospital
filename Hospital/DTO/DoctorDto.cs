using Hospital.Enum;

namespace Hospital.DTO;

public class DoctorDto
{
    public int Id { get; set; }
    public DoctorsType DoctorType { get; set; }
    public int? ScheduleId { get; set; }
}