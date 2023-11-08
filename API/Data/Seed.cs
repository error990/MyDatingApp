using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class Seed
{
    public static async Task SeedUsers(DataContext context)
    {
        if (await context.Users.AnyAsync()) return; // Are there any Users? If so, end Task

        // Read SeedFile --> Create a List of Users
        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        // Just in case the seed data from the json file has not the right casing
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        // Props in AppUser and Keys in Json have to match in order to be deserialized into AppUser objects! Typos can cause Properties not populated.
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options); 

        // Generate Password Hash for every User --> Pa$$w0rd
        foreach (AppUser user in users!)
        {
            using HMACSHA512 hmac = new(); // Randomly generated Key

            user.Username = user.Username.ToLower(); // why not in the Setter of AppUser?
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    }
}
