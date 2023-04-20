using System.Text.Encodings.Web;
using System.Text.Json;
using Hospital.Data;
using Hospital.DTO;
using Hospital.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;

namespace Hospital.Controllers;

[ApiController]

[Route("[controller]")]
public class DoctorsController: ControllerBase
{
    private readonly DoctorService _doctorService;

    public DoctorsController(DoctorService doctorService)
    {
        _doctorService = doctorService;
    }


    [HttpGet]
    [Route("doctors")]
    public async Task<List<DoctorDto>> GetDoctorsAsync()
    {
        var doctors = await _doctorService.GetDoctorsAsync();
        return doctors.Select(d => new DoctorDto()
        {
            Id = d.Id,
            DoctorType = d.DoctorType,
            ScheduleId = d.ScheduleId,
        }).ToList();
 
    }
    
    
    [HttpGet]
    [Route("doctor/{id}")]
    public async Task<IActionResult> GetDoctorAsync(int id)
    {
        var doctor = await _doctorService.GetDoctorAsync(id);
        if (doctor == null) return NotFound();
        var doctorDto = new DoctorDto()
        {
            Id = doctor.Id,
            DoctorType = doctor.DoctorType,
            ScheduleId = doctor.ScheduleId

        };
        return  Ok(doctorDto);
    }

    [HttpPost]
    [Route("doctor")]
    public async Task<IActionResult> CreateDoctor(DoctorDto doctorDto)
    {   
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var doctor = new Doctor()
        {
            Id = doctorDto.Id,
            DoctorType = doctorDto.DoctorType,
            ScheduleId = doctorDto.ScheduleId
            
        };
        await _doctorService.AddDoctorAsync(doctor);
        return Ok();
    }

    [HttpPut]
    [Route("doctor/{id}")]
    public async Task<IActionResult> PutDoctorAsync(int id, DoctorDto doctorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var doctor = new Doctor()
        {
            Id = doctorDto.Id,
            DoctorType = doctorDto.DoctorType,
            ScheduleId = doctorDto.ScheduleId
            
        };
        var result = await  _doctorService.UpdateDoctorAsync(id, doctor);
        if (result == false) return NotFound();
        return Ok();



    }

    [HttpDelete]
    [Route("doctor/{id}")]
    public async Task<IActionResult> DeleteDoctorAsync(int id)
    {
        var result = await _doctorService.RemoveDoctorAsync(id);
        if (result == false) return NotFound();
        return Ok();
    }



}