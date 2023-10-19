using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.Domain;

class Patient
{
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