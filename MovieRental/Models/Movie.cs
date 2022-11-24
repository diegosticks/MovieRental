using System.ComponentModel.DataAnnotations;

namespace MovieRental.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        [Required]
        [Display(Name ="Genre")]
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }

        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name ="Number in Stock")]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }
        public byte NumberAvailable { get; set; }
    }
}
