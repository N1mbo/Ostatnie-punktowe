using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ostatnie_punktowe.Models;
using OstatniePunktowe.Controllers;
using OstatniePunktowe.Data;
using OstatniePunktowe.DTOs;

namespace OstatniePunktowe.Tests;

public class PrescriptionControllerTest
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new AppDbContext(options);
        dbContext.Doctors.Add(new Doctor
            { IdDoctor = 1, FirstName = "Maja", LastName = "Wyspa", Email = "maja.wyspa@buzi.wp" });
        dbContext.Medicaments.Add(new Medicament
            { IdMedicament = 1, Name = "Na bol dupy", Description = "Na zazdrosc", Type = "Masc" });
        dbContext.SaveChanges();

        return dbContext;
    }

    [Fact]
    public async Task BadRequestAtMoreThen10Medicaments()
    {
        var db = GetDbContext();
        var controller = new PrescriptionsController(db);

        var request = new PrescriptionCreateRequestDTO
        {
            Patient = new PatientDTO
                { FirstName = "Patryk", LastName = "Pala", Birthdate = DateTime.Now.AddYears(-20) },
            Doctor = new DoctorDTO { IdDoctor = 1 },
            Date = DateTime.Now.AddDays(-20),
            DueDate = DateTime.Now.AddDays(10),
            Medicaments = new List<MedicamentDTO>()
        };

        for (var i = 0; i < 11; i++)
        {
            request.Medicaments.Add(new MedicamentDTO
            {
                IdMedicament = i + 1,
                Dose = 20,
                Details = "testy testy",

            });
        }

        var result = await controller.CreatePrescription(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task NotFoundWhenDoctorDoesNotExist()
    {
        var db = GetDbContext();
        var controller = new PrescriptionsController(db);

        var request = new PrescriptionCreateRequestDTO
        {
            Patient = new PatientDTO
                { FirstName = "Patryk", LastName = "Pala", Birthdate = DateTime.Now.AddYears(-20) },
            Doctor = new DoctorDTO { IdDoctor = 657 },
            Date = DateTime.Now.AddDays(-20),
            DueDate = DateTime.Now.AddDays(10),
            Medicaments = new List<MedicamentDTO>
            {
                new MedicamentDTO()
                {
                    IdMedicament = 1,
                    Dose = 20,
                    Details = "testy testy",
                }
            }
        };

        var result = await controller.CreatePrescription(request);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task ReturnOkWhenEverythingIsOk()
    {
        var db = GetDbContext();
        var controller = new PrescriptionsController(db);

        var request = new PrescriptionCreateRequestDTO
        {
            Patient = new PatientDTO { FirstName = "Patryk", LastName = "Pala", Birthdate = DateTime.Now.AddYears(-20) },
            Doctor = new DoctorDTO { IdDoctor = 1 },
            Date = DateTime.Now.AddDays(-20),
            DueDate = DateTime.Now.AddDays(10),
            Medicaments = new List<MedicamentDTO>
            {
                new MedicamentDTO()
                {
                    IdMedicament = 1,
                    Dose = 20,
                    Details = "testy testy",
                }
            }
        };
        
        var result = await controller.CreatePrescription(request);
        Assert.IsType<OkObjectResult>(result);
    }
}