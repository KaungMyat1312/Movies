using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCMovie.ViewModels;

public class MovieFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Title field is required.")]
    [StringLength(100, ErrorMessage = "The Title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Release Date field is required.")]
    [DataType(DataType.Date)]
    [Display(Name = "Release Date")]
    public DateTime ReleaseDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "The Duration field is required.")]
    [Range(1, 500, ErrorMessage = "Duration must be between 1 and 500 minutes.")]
    [Display(Name = "Duration (Minutes)")]
    public int Duration { get; set; }
    public int CategoryId { get; set; } 
    public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>? Categories { get; set; } 
    [Required(ErrorMessage = "The Rating field is required.")]
    public string? Rating { get; set; }

    [Display(Name = "Poster URL")]
    public string? PosterUrl { get; set; }

    [Display(Name = "Trailer URL")]
    public string? TrailerUrl { get; set; }

    [Display(Name = "Is Active")]
    public bool IsActive { get; set; } = true;

    public List<CategoryCheckBoxViewModel> AvailableCategories { get; set; } = new();
}

public class CategoryCheckBoxViewModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}