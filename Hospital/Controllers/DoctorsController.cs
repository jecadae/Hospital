using AutoMapper;
using Hospital.Data;
using Hospital.DTO;
using Hospital.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly DoctorService _doctorService;
    private readonly IMapper _mapper;
    
    public DoctorsController(DoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }
    
/// <summary>
/// Получить всех докторов
/// </summary>
/// <returns> лист докторов</returns>
    [HttpGet("doctors")]
public async Task<List<DoctorDto>> GetDoctorsAsync()
    {
        var doctors = await _doctorService.GetAllAsync(); return _mapper.Map<List<DoctorDto>>(doctors);
    }

    /// <summary>
    /// получение доктора по id
    /// </summary>
    /// <param name="id">id доктора</param>
    /// <returns>Доктора</returns>
    [HttpGet]
    [Route("doctor/{id}")]
    public async Task<IActionResult> GetDoctorAsync(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        if (doctor == null) 
            return NotFound();
        return Ok(_mapper.Map<DoctorDto>(doctor));
        
    }
    /// <summary>
    /// Создание доктора
    /// </summary>
    /// <param name="doctorDto">Модель доктора</param>
    /// <returns>ок</returns>
    [HttpPost]
    [Route("doctor")]
    public async Task<IActionResult> CreateDoctor(DoctorDto doctorDto)
    {
        var doctor = _mapper.Map<Doctor>(doctorDto);
        await _doctorService.CreateAsync(doctor);
        return Ok();
    }
    /// <summary>
    /// Изменить существующего доктора
    /// </summary>
    /// <param name="id">id доктора</param>
    /// <param name="doctorDto">Модель доктора</param>
    /// <returns><ок/returns>
    [HttpPut]
    [Route("doctor/{id}")]
    public async Task<IActionResult> PutDoctorAsync(int id, DoctorDto doctorDto)
    {
        var doctor = _mapper.Map<Doctor>(doctorDto);
        var result = await _doctorService.UpdateAsync(id, doctor);
        if (result == false) 
            return NotFound();
        return Ok();
    }
    /// <summary>
    /// удаление доктора
    /// </summary>
    /// <param name="id">id доктора</param>
    /// <returns>ок</returns>
    [HttpDelete]
    [Route("doctor/{id}")]
    public async Task<IActionResult> DeleteDoctorAsync(int id)
    {
        var result = await _doctorService.DeleteAsync(id);
        if (result == false) 
            return NotFound();
        return Ok();
    }


    [HttpGet]
    [Route("schedule/{id}")]
    public async Task<IActionResult> GetSchedule(int id)
    {
        var result = await _doctorService.GetDoctorScheduleAsync(id);
        if (result == null) 
            return NotFound();


        return Ok(result);
    }
}