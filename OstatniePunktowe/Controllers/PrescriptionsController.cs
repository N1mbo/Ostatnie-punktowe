using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ostatnie_punktowe.Models;
using OstatniePunktowe.Data;
using OstatniePunktowe.DTOs;

namespace OstatniePunktowe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PrescriptionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionCreateRequestDTO request)
    {
        if (request.Medicaments.Count > 10) 
            return BadRequest("Recepta może obejmować maksymalnie 10 leków.");

        if (request.DueDate < request.Date)
            return BadRequest("Termin recepty musi byc wiekszy od daty wystawienia");

        var doctor = await _context.Doctors.FindAsync(request.Doctor.IdDoctor);
        if (doctor == null) 
            return NotFound("Nie znaleziono podanego Doktora");
        
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => 
                p.FirstName == request.Patient.FirstName &&
                p.LastName == request.Patient.LastName &&
                p.Birthdate == request.Patient.Birthdate);

        if (patient == null)
        {
            patient = new Patient()
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            
            _context.Patients.Add(patient);
        }

        var medicamentList = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var foundMedicament = await _context.Medicaments
            .Where(m => medicamentList.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();
        
        var missing = medicamentList.Except(foundMedicament).ToList();
        if (missing.Any())
            return NotFound($"Podane leki nie istnieja: {string.Join(", ", missing)}");

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Doctor = doctor,
            Patient = patient,
            PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details ?? ""
            }).ToList()
        };
        
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok("Pomyslnie utworzono recepte");
    }
}