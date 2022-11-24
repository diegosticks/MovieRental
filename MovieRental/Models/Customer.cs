using System.ComponentModel.DataAnnotations;

namespace MovieRental.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }

        [Display(Name ="Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Display(Name ="Date of Birth Format: d mmm yyyy")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}
