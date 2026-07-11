using System;

namespace MVCMovie.Models;

public class UserSave
{
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }

    public int MovieId { get; set; }
    public virtual Movie? Movie { get; set; }

    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
}