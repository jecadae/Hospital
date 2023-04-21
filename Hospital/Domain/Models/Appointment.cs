using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entity;

/// <summary>
/// Модель записи
/// </summary>
public class Appointment
{
    /// <summary>
    /// 
    /// </summary>
    public int? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int DoctorId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long InsuranceNumberId { get; set; }

    public DateTime StartVisit { get; set; }
    public DateTime EndVisit { get; set; }
    public bool IsFired { get; set; }
    public Doctor Doctor { get; set; }
    [ForeignKey(nameof(InsuranceNumberId))]
    public Patient Patient { get; set; }
}