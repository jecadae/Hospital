using System.ComponentModel.DataAnnotations;

namespace Hospital.Entity;

public class Patient
{
    [Key] public long Polis { get; set; }

    public string Name { get; set; }
    public int Age { get; set; }
    public bool Status { get; set; } = false;
}