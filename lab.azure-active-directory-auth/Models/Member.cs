using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lab.azure_active_directory_auth.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public string UserId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(256)]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(256)]
        public string EmailAddress { get; set; }

        [DisplayName("Phone")]
        //[Required(ErrorMessage = "Phone is required")]
        [MaxLength(256)]
        public string PhoneNumber { get; set; }

        [DisplayName("Present Address")]
        //[Required(ErrorMessage = "Present Address is required")]
        [MaxLength(500)]
        public string PresentAddress { get; set; }

        [DisplayName("Permanent Address")]
        //[Required(ErrorMessage = "Permanent Address is required")]
        [MaxLength(500)]
        public string PermanentAddress { get; set; }
    }
}
