using AutoMapper;
using Hospital.Data;
using Hospital.DTO;
using Hospital.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers;

[Route("[controller]")]
public class SchedulesController: ControllerBase
{
    private readonly IHospitalService<Schedule> _scheduleService;
    private readonly IMapper _mapper;
    
    public SchedulesController(IHospitalService<Schedule>  scheduleService, IMapper mapper)
    {
        _scheduleService = scheduleService;
        _mapper = mapper;
    }
    
    [HttpGet("schedules")]
    public async Task<List<ScheduleDto>> GetSchedulesAsync()
    {
        var schedules = await _scheduleService.GetAllAsync(); return _mapper.Map<List<ScheduleDto>>(schedules);
    }

    
    [HttpGet]
    [Route("schedule/{id}")]
    public async Task<IActionResult> GetSchedulesAsync(int id)
    {
        var schedule = await _scheduleService.GetByIdAsync(id);
        if (schedule == null) 
            return NotFound();
        return Ok(_mapper.Map<ScheduleDto>(schedule));

        
    }

    
    [HttpPost]
    [Route("schedule")]
    public async Task<IActionResult> CreateSchedule(ScheduleDto scheduleDto)
    {
        var schedule = _mapper.Map<Schedule>(scheduleDto);
        await _scheduleService.CreateAsync(schedule);
        return Ok();
    }

    [HttpPut]
    [Route("schedule/{id}")]
    public async Task<IActionResult> PutScheduleAsync(int id, ScheduleDto scheduleDto)
    {
        var schedule = _mapper.Map<Schedule>(scheduleDto);
        var result = await _scheduleService.UpdateAsync(id, schedule);
        if (result == false) 
            return NotFound();
        return Ok();
    }

    [HttpDelete]
    [Route("Schedule/{id}")]
    public async Task<IActionResult> DeleteScheduleAsync(int id)
    {
        var result = await _scheduleService.DeleteAsync(id);
        if (result == false) 
            return NotFound();
        return Ok();
    }
    
    
    
}