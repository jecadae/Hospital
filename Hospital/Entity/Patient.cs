using System.ComponentModel.DataAnnotations;

namespace Hospital.Entity;

public class Patient
{
    [Key] 
    public int InsuranceNumberId { get; set; }

    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsFired { get; set; }
}