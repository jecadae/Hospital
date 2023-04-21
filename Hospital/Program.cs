using Hospital.Data;
using Hospital.Entity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var DefaultString = builder.Configuration.GetConnectionString("DefaultString");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(DefaultString));
builder.Services.AddControllers();
builder.Services.AddScoped<IHospitalService<Patient>, PatientService>();
builder.Services.AddScoped<IHospitalService<Doctor>, DoctorService>();
builder.Services.AddScoped<IHospitalService<Appointment>, AppointmentsService>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

/*app.Configuration["name"] = "Tom";
var df = builder.Configuration.GetSection("name");
Console.WriteLine("Section name: {0}", df.Value);*/