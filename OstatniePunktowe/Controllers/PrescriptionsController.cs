using Microsoft.AspNetCore.Mvc;
using Ostatnie_punktowe.Models;
using OstatniePunktowe.Data;
using OstatniePunktowe.DTOs;

namespace OstatniePunktowe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController
{
    private readonly AppDbContext _context;

    public PrescriptionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionCreateRequestDTO request)
    {
        if(request.Medicaments.Count > 10) 
    }
}