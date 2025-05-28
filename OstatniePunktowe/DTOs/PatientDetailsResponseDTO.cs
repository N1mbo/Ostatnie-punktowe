using System.ComponentModel.DataAnnotations;
using Ostatnie_punktowe.Models;

namespace OstatniePunktowe.DTOs;

public class PatientDetailsResponseDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    
    public ICollection<PrescriptionResponseDTO> Prescriptions { get; set; }
}

public class PrescriptionResponseDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public DoctorBasicDTO Doctor { get; set; }
    public List<MedicamentDetailsDTO> Medicaments { get; set; }
}

public class DoctorBasicDTO
{
    [Range(1, int.MaxValue, ErrorMessage = "IdDoctor musi byc wieksze od 0")]
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class MedicamentDetailsDTO
{
    [Range(1, int.MaxValue, ErrorMessage = "IdMedicament musi byc wieksze od 0")]
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    [Range(1, 1000, ErrorMessage = "Nie przedawkuj biedakowi")]
    public int Dose { get; set; }
    public string Details { get; set; }
}