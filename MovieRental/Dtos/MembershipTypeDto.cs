using System.ComponentModel.DataAnnotations;

namespace MovieRental.Dtos
{
    public class MembershipTypeDto
    {
        [Key]
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
