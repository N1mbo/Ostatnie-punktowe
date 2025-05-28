using Ostatnie_punktowe.Models;

namespace OstatniePunktowe.DTOs;

public class PatientDetailsResponseDTO
{
    public int idPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    
    public ICollection<PrescriptionResponseDTO> Prescriptions { get; set; }
}

public class PrescriptionResponseDTO
{
    public int idPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public DoctorBasicDTO Doctor { get; set; }
    public List<MedicamentDetailsDTO> Medicaments { get; set; }
}

public class DoctorBasicDTO
{
    public int idDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class MedicamentDetailsDTO
{
    public int idMedicament { get; set; }
    public string Name { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}