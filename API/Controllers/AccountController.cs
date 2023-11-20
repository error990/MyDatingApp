using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }


    // using: to call the hmac.Dispose() method automatically when exiting the Register() method
    // HMACSHA512 derives from a class that implements IDispsable --> so we can use "using"
    // hmac acts as SALT
    [HttpPost("register")] // POST: api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
    {
        if(await UserExists(dto.Username)) return BadRequest("Username is already taken.");

        using var hmac = new HMACSHA512();

        AppUser user = new AppUser
        {
            Username = dto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDto(
            dto.Username.ToLower(), 
            _tokenService.CreateToken(user),
            string.Empty
        );
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto dto)
    {
        AppUser? user = await _context.Users
            .Include(u => u.Photos)
            .SingleOrDefaultAsync(u => u.Username.ToLower() == dto.Username.ToLower());

        if (user is null ) return Unauthorized("invalid username");

        // Try to get the right hash with the passwordSalt stored in db
        using var hmac = new HMACSHA512(user.PasswordSalt);
        // Password Byte[] of dto password
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

        // compare the Byte[]s
        for (int i=0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
        }

        return new UserDto(
            dto.Username, 
            _tokenService.CreateToken(user),
            user.Photos.FirstOrDefault(ph => ph.IsMain)?.Url ?? string.Empty
        );
    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
    }
}
