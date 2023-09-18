using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.Domain;

class Patient
{
    // Konvention i EF Core är att om du lägger till ett en publik property med namn
    // Id eller PatientId så kommer denna automatiskt utses vara primärnyckel. 
    // Kan annars använda [Key] för att specificera primärnyckel.
    public int Id { get; set; }

    [MaxLength(50)]
    public required string FirstName { get; set; }

    [MaxLength(50)]
    public required string LastName { get; set; }

    [Column(TypeName = "nchar(13)")]
    public required string SocialSecurityNumber { get; set; }

    [MaxLength(20)]    
    public required string Phone { get; set; }

    [MaxLength(50)]    
    public required string Email { get; set; }
}