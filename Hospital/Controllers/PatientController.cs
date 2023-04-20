using Hospital.Data;
using Hospital.DTO;
using Hospital.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers;

[Route("[controller]")]
public class PatientController: ControllerBase
{
    private readonly PatientService _patientService;

    public PatientController(PatientService patientService)
    {
        _patientService = patientService;
    }
    
    
    
    [HttpGet]
    [Route("patients")]
    public async Task<List<PatientDto>> GetPatientsAsync()
    {
        var patients =  await _patientService.GetPatientsAsync();
        return patients.Select(d => new PatientDto()
        {
            Polis = d.Polis,
            Name = d.Name,
            Age= d.Age,
        }).ToList();
    }
    
    [HttpGet]
    [Route("patient/{polis}")]
    public async Task<IActionResult> GetPatientAsync(long polis)
    {
        var patient = await _patientService.GetPatientAsync(polis);
        if (patient == null) return NotFound();
        var patientDto = new PatientDto()
        {
            Polis = patient.Polis,
            Name = patient.Name,
            Age= patient.Age,

        };
        return  Ok(patientDto);
    }
    
    
    [HttpPost]
    [Route("patient")]
    public async Task<IActionResult> CreatePatient(PatientDto patientDto)
    {   
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var patient = new Patient()
        {
            Polis = patientDto.Polis,
            Name = patientDto.Name,
            Age= patientDto.Age,

        };
            

        await _patientService.AddPatientAsync(patient);
        return Ok();
    }

    [HttpPut]
    [Route("patient/{polis}")]
    public async Task<IActionResult> PutPatientAsync(long polis, PatientDto patientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var patient = new Patient()
        {
            Polis = patientDto.Polis,
            Name = patientDto.Name,
            Age = patientDto.Age,

        };

        var result = await _patientService.UpdatePatientAsync(polis, patient);
        if (result == false) return NotFound();
        return Ok();


    }

    [HttpDelete]
    [Route("patient/{polis}")]
    public async Task<IActionResult> DeleteDoctorAsync(long polis)
    {
        var result = await _patientService.RemovePatientAsync(polis);
        if (result == false) return NotFound(); 
        return Ok();
    }


    [HttpGet]
    [Route("Appontment/{polis}")]
    public async Task<IActionResult> AllPatientAppontmentAsync(long polis)
    {
        var patient = await _patientService.GetPatientAsync(polis);
        if (patient.Name == null) return NotFound();
        var Appointments = await _patientService.GetAllPatientAppointmentAsync(polis);

        return Ok(Appointments.Select(d => new AppontmentDto()
            {
                Id = d.Id,
                DoctorId = d.DoctorId,
                PatientPolis = d.PatientPolis,
                StartVisit = d.StartVisit,
                EndVisit = d.EndVisit
            }
        ).ToList());


    }
        /// <summary>
        /// Все записи что предстоит посетить
        /// </summary>
        /// <param name="polis"> Полис пациента</param>
        /// <returns> Лист записей</returns>
        [HttpGet]
        [Route("Appontment/{polis}")]
        public async Task<IActionResult> AllPatientAppontmentToVisitAsync(long polis)
        {
            var patient = await _patientService.GetPatientAsync(polis);
            if (patient.Name == null) return NotFound();
            var Appointments = await _patientService.GetAllPatientAppointmentToVisitAsync(polis);

            return Ok(Appointments.Select(d => new AppontmentDto()
                {
                    Id = d.Id,
                    DoctorId = d.DoctorId,
                    PatientPolis = d.PatientPolis,
                    StartVisit = d.StartVisit,
                    EndVisit = d.EndVisit
                }
            ).ToList());
        }
        

}