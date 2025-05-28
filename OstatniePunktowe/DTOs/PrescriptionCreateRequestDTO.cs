using Ostatnie_punktowe.Models;

namespace OstatniePunktowe.DTOs;

public class PrescriptionCreateRequestDTO
{
    public DoctorDTO Doctor { get; set; }
    public PatientDTO Patient { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public List<MedicamentDTO> Medicaments { get; set; } = new();
}

public class DoctorDTO
{
    public int IdDoctor { get; set; }
}
public class PatientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}
public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}