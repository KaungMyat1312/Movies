using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MVCMovie.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 500)]
        public int Duration { get; set; }
        [Display(Name = "Poster")]
        public string? PosterUrl { get; set; }

        [Display(Name = "Trailer")]
        public string? TrailerUrl { get; set; }

        [Required]
        [StringLength(10)]
        public string Rating { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public virtual ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();

        public virtual ICollection<UserFavourite> UserFavourites { get; set; } = new List<UserFavourite>();
        public virtual ICollection<UserSave> UserSaves { get; set; } = new List<UserSave>();



    }
}
