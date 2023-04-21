using System.ComponentModel.DataAnnotations;

namespace Hospital.Entity;
/// <summary>
/// Модель пациента
/// </summary>
public class Patient
{
    [Key] 
    public int InsuranceNumberId { get; set; }

    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsFired { get; set; }
    public Appointment Appointment{ get; set; }
}