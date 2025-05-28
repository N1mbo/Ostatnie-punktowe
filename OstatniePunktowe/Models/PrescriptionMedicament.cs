using System.ComponentModel.DataAnnotations;

namespace Ostatnie_punktowe.Models;

public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; }
    
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }
    
    [Required]
    public int Dose { get; set; }
    [Required]
    public string Details { get; set; }
    
}