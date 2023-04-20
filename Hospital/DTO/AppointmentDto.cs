namespace Hospital.DTO;

public class AppointmentDto
{
    public int? Id { get; set; }
    public int DoctorId { get; set; }
    public long InsuranceNumberId { get; set; }
    public DateTime StartVisit { get; set; }
    public DateTime EndVisit { get; set; }
}