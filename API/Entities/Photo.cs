using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")] // --> Annotation to change the table name from "Photo" to "Photos" without the use of a DBSet in the Context
public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public string PublicId { get; set; } = string.Empty; // --> Cloudinary ID

    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
}