using System.ComponentModel.DataAnnotations;

namespace API.DTOs;
public record RegisterDto
(
    [Required]
    string Username,
    [Required]
    [StringLength(8, MinimumLength = 4)]
    string Password
) { }