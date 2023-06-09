using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entity;

public class Appointment
{
    public int Id{ get; set; }
    public int DoctorId{ get; set; }
    [ForeignKey("Fk_Patient")] 
    public long PatientPolis{ get; set; }
    public DateTime StartVisit{ get; set; }
    public DateTime EndVisit{ get; set; }
    
    
    
    
    
    public bool Status { get; set; } = false;
    public Doctor Doctor{ get; set; }
    public Patient Patient{ get; set; }
    
}