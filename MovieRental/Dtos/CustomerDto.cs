using MovieRental.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MovieRental.Dtos
{
    public class CustomerDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipTypeDto? MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}
