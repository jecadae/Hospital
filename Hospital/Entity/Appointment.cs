using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entity;

public class Appointment
{
    public int? Id { get; set; }
    public int DoctorId { get; set; }

    [ForeignKey(nameof(InsuranceNumberId))]
    public long InsuranceNumberId { get; set; }

    public DateTime StartVisit { get; set; }
    public DateTime EndVisit { get; set; }


    public bool IsFired { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
}