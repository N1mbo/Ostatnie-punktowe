using System.ComponentModel.DataAnnotations;

namespace Ostatnie_punktowe.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}