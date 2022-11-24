using MovieRental.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MovieRental.Dtos
{
    public class MovieDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Genre? Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Range(1, 20)]
        public byte NumberInStock { get; set; }
    }
}
