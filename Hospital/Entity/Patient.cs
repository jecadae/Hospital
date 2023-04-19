using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Entity;

public class Patient
{
    [Key]
    public long Polis{ get; set; }
    public string Name{ get; set; }
    public int Age{ get; set; }
    public bool Status { get; set; } = false;
}