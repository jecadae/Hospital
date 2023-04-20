namespace Hospital.DTO;

public class ScheduleDto
{
    public int DoctorId { get; set; }
    public int Id { get; set; }
    public DateTime BeginDay { get; set; }
    public DateTime EndDay { get; set; }
    public DateTime BeginLunch { get; set; }
    public DateTime EndLunch { get; set; }
    public int ReceptionInterval { get; set; }
    public List<DayOfWeek> WeekDay { get; set; }
}