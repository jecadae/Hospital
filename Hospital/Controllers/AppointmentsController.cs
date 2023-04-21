using AutoMapper;
using Hospital.Data;
using Hospital.DTO;
using Hospital.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers;


[ApiController]
[Route("[controller]")]
public class AppointmentsController: ControllerBase
{
    private readonly IHospitalService<Appointment> _appointmentService;
    private readonly IMapper _mapper;
    
    public AppointmentsController(IHospitalService<Appointment> appointmentService, IMapper mapper)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;
    }
    [HttpGet]
    [Route("Appointments")]
    public async Task<List<AppointmentDto>> GetAppointmentsAsync()
    {
        var appointments = await _appointmentService.GetAllAsync();

        return _mapper.Map<List<AppointmentDto>>(appointments);
    }
    
    [HttpGet]
    [Route("Appointments/{id}")]
    public async Task<AppointmentDto> GetAppointmentAsync(int id)
    {
        var appointment = await _appointmentService.GetByIdAsync(id);

        return _mapper.Map<AppointmentDto>(appointment);
    }
    
    
    /// <summary>
    ///     Создание записи
    /// </summary>
    /// <param name="appointmentDto">модель записи</param>
    /// <returns>Ок если объект был создан, badrequest если нет</returns>
    [HttpPost]
    [Route("Appointment")]
    public async Task<IActionResult> CreateAppointmentAsync(AppointmentDto appointmentDto)
    {
        var appointment = _mapper.Map<Appointment>(appointmentDto);
        var result = await _appointmentService.CreateAsync(appointment);
        if (result == false) 
            return BadRequest();
        return Ok();
    }
    /// <summary>
    /// удалить запись
    /// </summary>
    /// <param name="id"> id записи</param>
    /// <returns>ок</returns>
    [HttpDelete]
    [Route("Appointment/{id}")]
    public async Task<IActionResult> RemoveAppointmentAsync(int id)
    {
        var result = await _appointmentService.DeleteAsync(id);
        if (result == false) 
            return BadRequest();
        return Ok();
    }
    
    [HttpPut]
    [Route("Appointment/{id}")]
    public async Task<IActionResult> UpdateAppointmentAsync(int id ,AppointmentDto appointmentDto)
    {
        var appointment = _mapper.Map<Appointment>(appointmentDto);
        var result = await _appointmentService.UpdateAsync(id,appointment);
        if (result == false) 
            return BadRequest();
        return Ok();
    }
    
    
    

    
}