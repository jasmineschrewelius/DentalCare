using DentalCare.Data;
using DentalCare.Domain;
using static System.Console;

namespace DentalCare;

class Program
{
    public static void Main()
    {
        Title = "Dental Care";

        while (true)
        {
            CursorVisible = false;

            WriteLine("1. Registrera patient");
            WriteLine("2. Sök patient");
            WriteLine("3. Uppdatera patient");

            var keyPressed = ReadKey(true);

            Clear();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    RegisterPatientView();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    
                    SearchPatientView();
                    
                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    
                    UpdatePatientView();
                    
                    break;
            }

            Clear();
        }
    }

    private static void UpdatePatientView()
    {
        var socialSecurityNumber = GetUserInput("Personnummer");

        var patient = FindPatient(socialSecurityNumber);

        if (patient is not null)
        {
            WriteLine($"{patient.FirstName} {patient.LastName}, {patient.SocialSecurityNumber}");

            WriteLine(new string('-', 60));

            using var context = new ApplicationDbContext();

            context.Patient.Attach(patient);

            patient.FirstName = GetUserInput("Förnamn");
            patient.LastName = GetUserInput("Efternamn");
            patient.Phone = GetUserInput("Telefonnummer");
            patient.Email = GetUserInput("E-post");

            context.SaveChanges();

            WriteLine("Patient sparad");
        }
        else
        {
            WriteLine("Patient saknas");
        }

        Thread.Sleep(2000);
    }

    private static void SearchPatientView()
    {
        var socialSecurityNumber = GetUserInput("Personnummer");

        var patient = FindPatient(socialSecurityNumber);

        if (patient is not null)
        {
            WriteLine($"Förnamn: {patient.FirstName}");
            WriteLine($"Efteramn: {patient.LastName}");
            WriteLine($"Personnummer: {patient.SocialSecurityNumber}");
            WriteLine($"Telefonnummer: {patient.Phone}");
            WriteLine($"E-post: {patient.Email}");

            WaitUntilKeyPressed(ConsoleKey.Escape);
        }
        else
        {
            WriteLine("Patient saknas");
        }
    }

    private static void WaitUntilKeyPressed(ConsoleKey key)
    {
        while (ReadKey(true).Key != key);
    }

    private static Patient? FindPatient(string socialSecurityNumber)
    {
        using var context = new ApplicationDbContext();

        var patient = context.Patient.FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);

        return patient;
    }

    private static void RegisterPatientView()
    {
        var firstName = GetUserInput("Förnamn");
        var lastName = GetUserInput("Efternamn");
        var socialSecurityNumber = GetUserInput("Personnummer");
        var phone = GetUserInput("Telefonnummer");
        var email = GetUserInput("E-post");

        var patient = new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            SocialSecurityNumber = socialSecurityNumber,
            Phone = phone,
            Email = email
        };

        SavePatient(patient);

        Clear();

        WriteLine("Patient sparad");

        Thread.Sleep(2000);
    }

    private static void SavePatient(Patient patient)
    {
        using var context = new ApplicationDbContext();

        context.Patient.Add(patient);

        // Här räknar DbContext ut vad som behöver ske i databasen för att säkerställa
        // att data vi för tillfället enbart har i minnet, ska synkas med databasen - i detta 
        // fallet innebär det att en INSERT INTO kommer genereras och skickas till databasen.-
        context.SaveChanges();
    }

    private static string GetUserInput(string label)
    {
        Write($"{label}: ");

        return ReadLine() ?? "";
    }
}
