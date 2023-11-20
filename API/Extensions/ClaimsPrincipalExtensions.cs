using System.Security.Claims;

namespace API.Extensions;
// To get the username just by calling this method
public static class ClaimsPrincipalExtensions
{
    public static string GetUserName(this ClaimsPrincipal user)
    {        
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }
}
