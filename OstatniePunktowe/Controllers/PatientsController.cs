using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ostatnie_punktowe.Models;
using OstatniePunktowe.Data;
using OstatniePunktowe.DTOs;

namespace OstatniePunktowe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(AppDbContext _context) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctor)
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.PrescriptionMedicaments)
                    .ThenInclude((pm => pm.Medicament))
            .FirstOrDefaultAsync(p => p.IdPatient == id);
        
        if (patient == null)
            return NotFound("Pacjent nie zleziony");

        var response = new PatientDetailsResponseDTO
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionResponseDTO
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorBasicDTO
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDetailsDTO()
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Medicament.Description,
                        Details = pm.Details,
                        Type = pm.Medicament.Type,
                    }).ToList()
                }).ToList()
        };
        
        return Ok(response);
    }
}