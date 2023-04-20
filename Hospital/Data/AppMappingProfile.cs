using AutoMapper;
using Hospital.DTO;
using Hospital.Entity;

namespace Hospital.Data;
/// <summary>
/// конфиг маппера
/// </summary>
public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Schedule, ScheduleDto>().ReverseMap();
    }
}