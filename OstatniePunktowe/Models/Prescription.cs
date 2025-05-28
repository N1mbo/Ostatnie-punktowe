using System.ComponentModel.DataAnnotations;

namespace Ostatnie_punktowe.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public int IdDoctor { get; set; }
    
    public ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; } = new List<Prescription_Medicament>();
}