using Hospital.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    public DbSet<Appointment> Appointments{ get; set; }
    public DbSet<Doctor> Doctors{ get; set; }
    public DbSet<Patient> Patients{ get; set; }
    public DbSet<Schedule> Schedules{ get; set; }


}