using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab.azure_active_directory_auth.ViewModels
{
    public class MemberViewModel
    {
        [Key]
        public int MemberId { get; set; }

        public string? UserId { get; set; }

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
        public string? PhoneNumber { get; set; }

        [DisplayName("Present Address")]
        //[Required(ErrorMessage = "Present Address is required")]
        [MaxLength(500)]
        public string? PresentAddress { get; set; }

        [DisplayName("Permanent Address")]
        //[Required(ErrorMessage = "Permanent Address is required")]
        [MaxLength(500)]
        public string? PermanentAddress { get; set; }


        [DisplayName("Card Full Name")]
        [MaxLength(256)]
        public string? CardName { get; set; }

        [DisplayName("Card Number")]
        [MaxLength(256)]
        public string? CardNumber { get; set; }

        [DisplayName("Card Address")]
        [MaxLength(128)]
        public string? CardExpirationYear { get; set; }

        [DisplayName("Card Address")]
        [MaxLength(128)]
        public string? CardExpirationMonth { get; set; }

        [DisplayName("Card Cvc")]
        [MaxLength(128)]
        public string? CardCvc { get; set; }

        [DisplayName("Card Country")]
        [MaxLength(128)]
        public string? CardCountry { get; set; }

        [NotMapped]
        public string? FullName => (FirstName + " " + LastName).Trim();
    }
}
