namespace API.Entities;
public class AppUser
{
    public int Id { get; private set; }
    public string Username { get; set; } = string.Empty!;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
}