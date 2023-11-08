namespace API.DTOs;

// default values because AutoMapper made problems with PhotoUrl and not having a parameterless ctor...
public record MemberDto
(
    int Id = 0,
    string Username = "",
    string PhotoUrl = "", // Main Photo
    int Age = -1,
    string KnownAs = "",
    DateTime Created = default,
    DateTime LastActive = default,
    string Gender = "",
    string Introduction = "",
    string LookingFor = "",
    string Interests = "",
    string City = "",
    string Country = "",
    List<PhotoDto> Photos = default!
) { }