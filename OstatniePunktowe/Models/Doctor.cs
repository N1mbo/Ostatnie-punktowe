using System.ComponentModel.DataAnnotations;

namespace Ostatnie_punktowe.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}