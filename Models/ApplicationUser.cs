using Microsoft.AspNetCore.Identity;
using System;

namespace MVCMovie.Models;

public class ApplicationUser : IdentityUser
{
    public string? HomeTown { get; set; }
    public DateTime? BirthDate { get; set; }
    public virtual ICollection<UserFavourite> UserFavourites { get; set; } = new List<UserFavourite>();
    public virtual ICollection<UserSave> UserSaves { get; set; } = new List<UserSave>();
}