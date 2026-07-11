using System.ComponentModel.DataAnnotations.Schema;

namespace MVCMovie.Models
{
    public class MovieCategory
    {
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie? Movie { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}