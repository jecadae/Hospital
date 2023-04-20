using AutoMapper;
using Hospital.Data;
using Hospital.DTO;
using Hospital.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers;

[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly PatientService _patientService;

    public PatientController(PatientService patientService, IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("patients")]
    public async Task<List<PatientDto>> GetPatientsAsync()
    {
        var patients = await _patientService.GetAllAsync();
        return _mapper.Map<List<PatientDto>>(patients);
    }

    [HttpGet]
    [Route("patient/{InsuranceNumberId}")]
    public async Task<IActionResult> GetPatientAsync(int InsuranceNumberId)
    {
        var patient = await _patientService.GetByIdAsync(InsuranceNumberId);
        if (patient == null) return NotFound();
        var patientDto = _mapper.Map<PatientDto>(patient);
        return Ok(patientDto);
    }


    [HttpPost]
    [Route("patient")]
    public async Task<IActionResult> CreatePatient(PatientDto patientDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var patient = _mapper.Map<Patient>(patientDto);


        await _patientService.CreateAsync(patient);
        return Ok();
    }

    [HttpPut]
    [Route("patient/{InsuranceNumberId}")]
    public async Task<IActionResult> PutPatientAsync(int InsuranceNumberId, PatientDto patientDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var patient = _mapper.Map<Patient>(patientDto);

        var result = await _patientService.UpdateAsync(InsuranceNumberId, patient);
        if (result == false) return NotFound();
        return Ok();
    }

    [HttpDelete]
    [Route("patient/{InsuranceNumberId}")]
    public async Task<IActionResult> DeleteDoctorAsync(int InsuranceNumberId)
    {
        var result = await _patientService.DeleteAsync(InsuranceNumberId);
        if (result == false) return NotFound();
        return Ok();
    }


    [HttpGet]
    [Route("Appointment/{InsuranceNumberId}")]
    public async Task<IActionResult> AllPatientAppointmentAsync(int InsuranceNumberId)
    {
        var patient = await _patientService.GetByIdAsync(InsuranceNumberId);
        if (patient.Name == null) return NotFound();
        var appointments = await _patientService.GetAllPatientAppointmentAsync(InsuranceNumberId);

        return Ok(_mapper.Map<List<AppointmentDto>>(appointments));
    }

    /// <summary>
    ///     Все записи что предстоит посетить
    /// </summary>
    /// <param name="InsuranceNumberId"> Полис пациента</param>
    /// <returns> Лист записей</returns>
    [HttpGet]
    [Route("AppointmentsToVisit/{InsuranceNumberId}")]
    public async Task<IActionResult> AllPatientAppointmentToVisitAsync(int InsuranceNumberId)
    {
        var patient = await _patientService.GetByIdAsync(InsuranceNumberId);
        if (patient.Name == null) return NotFound();
        var appointments = await _patientService.GetAllPatientAppointmentToVisitAsync(InsuranceNumberId);

        return Ok(_mapper.Map<List<AppointmentDto>>(appointments));
    }

    /// <summary>
    ///     Создание записи
    /// </summary>
    /// <param name="appointmentDto">модель записи</param>
    /// <returns>Ок если объект был создан, badrequest если не гуд</returns>
    [HttpPost]
    [Route("Appointment")]
    public async Task<IActionResult> CreateAppointmentAsync(AppointmentDto appointmentDto)
    {
        var appointment = _mapper.Map<Appointment>(appointmentDto);
        var result = await _patientService.CreateAppointmentAsync(appointment);
        if (result == false) return BadRequest();
        return Ok();
    }

    [HttpDelete]
    [Route("Appointment/{id}")]
    public async Task<IActionResult> RemoveAppointmentAsync(int id)
    {
        var result = await _patientService.DeleteAppointmentAsync(id);
        if (result == false) return BadRequest();
        return Ok();
    }
    
    
    
}