namespace Hospital.Entity;

public class Doctor
{
    public int Id { get; set; }
    public Cpecies DoctorType { get; set; }
    public int? ScheduleId { get; set; }
    public bool Status { get; set; } = false;
}