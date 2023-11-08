using API.Extensions;

namespace API.Entities;
public class AppUser
{
    public int Id { get; private set; }
    public string Username { get; set; } = string.Empty!;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public string KnownAs { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string Gender { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public string LookingFor { get; set; } = string.Empty;
    public string Interests { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public List<Photo> Photos { get; set; } = new();

    // AutoMapper automatically maps "int GetWhatever()" to "int Whatever"!
    // AutoMapper queries the whole entity from the db if a method is used to populate a prop!
    // public int GetAge() 
    // {
    //     // CalculateAge() --> Custom Extension Method
    //     return DateOfBirth.CalculateAge();
    // }
}