namespace Hospital.Entity;
/// <summary>
/// Hcfc
/// </summary>
public class Schedule
{
    public int DoctorId{ get; set; }
    public int Id{ get; set; }
    public DateTime BeginDay{ get; set; }
    public DateTime EndDay{ get; set; }
    public DateTime BeginLunch{ get; set; }
    public DateTime EndLunch{ get; set; }
    public int TimeValue{ get; set; }
    public DayOfWeek WeekDay{ get; set; }
    
    
    public Doctor Doctor{ get; set; }
    public bool Status { get; set; } = false;
}