namespace Hospital.Entity;

/// <summary>
///     Модель расписания
/// </summary>
public class Schedule
{
    public int DoctorId { get; set; }
    public int Id { get; set; }
    public DateTime BeginDay { get; set; }
    public DateTime EndDay { get; set; }
    public DateTime BeginLunch { get; set; }
    public DateTime EndLunch { get; set; }
    public int ReceptionInterval { get; set; }
    public List<DayOfWeek> WeekDay { get; set; }

    public Doctor Doctor { get; set; }
    public bool IsFired { get; set; }
}