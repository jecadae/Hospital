using System.Text.Json.Serialization;
using Hospital.Entity;

namespace Hospital.DTO;

public class DoctorDto
{
    public int Id{ get; set; }
    public Cpecies DoctorType{ get; set; }
    public int? ScheduleId{ get; set; }


}
