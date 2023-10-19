using DentalCare.Domain;
using Microsoft.EntityFrameworkCore;

namespace DentalCare.Data;

// För att använda DbContext behöver vi installera paket 
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
class ApplicationDbContext : DbContext
{
    private string connectionString = "Server=.;Database=DentalCare;Trusted_Connection=True;Encrypt=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    // Enligt konvention i EF Core kommer det förväntas att det finns en tabell med namn Patient
    // som har samma antal kolumner som det finns publika properties i Patient.
    public DbSet<Patient> Patient  { get; set; }
}