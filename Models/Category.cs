using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCMovie.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;


        [StringLength(500)]
        public string? Description { get; set; }

        public virtual ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();

    }
}