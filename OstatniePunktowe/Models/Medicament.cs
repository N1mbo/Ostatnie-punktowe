using System.ComponentModel.DataAnnotations;

namespace Ostatnie_punktowe.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    [Required]
    [MaxLength(100)]
    public string Type { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}