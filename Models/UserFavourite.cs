using System;
using System.ComponentModel.DataAnnotations;

namespace MVCMovie.Models;

public class UserFavourite
{
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }

    public int MovieId { get; set; }
    public virtual Movie? Movie { get; set; }

    public DateTime FavoritedAt { get; set; } = DateTime.UtcNow; 
}