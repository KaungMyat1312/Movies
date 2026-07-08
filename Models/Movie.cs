using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MVCMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required,StringLength(60,MinimumLength =3)]
        public string ? Title { get; set; }

        [Display(Name ="Release Date"),]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}" , ApplyFormatInEditMode=true)]

        public DateTime ReleaseDate { get; set; }

        [Required, RegularExpression(@"^[A-Z]+[a-zA-Z]*$"), StringLength(30)]
        public string ? Genre { get; set; }

        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName="decimal(18,2)")]
        public decimal Price { get; set; }

        public string ? Rating { get; set; }
    }
}
