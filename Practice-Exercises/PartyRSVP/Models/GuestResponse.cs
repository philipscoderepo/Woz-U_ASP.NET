using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PartyRSVP.Models{
    public class GuestResponse
    {
        public int Id { get; set; }
        [MinLength(3)] [MaxLength(10)]
        public string FirstName {get; set;}
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Display(Description ="I can Attend")]
        public bool? WillAttend { get; set; }
        public string FullName(){
            return this.FirstName + " " + this.LastName;
        }
    }
}