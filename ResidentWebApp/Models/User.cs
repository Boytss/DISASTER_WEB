using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentWebApp.Models
{
    [Table("Resident", Schema = "dbo")]
    public class User
    {
         public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        MobileNumber = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
    }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Purok number is required")]
        public string? PurokNumber { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Mobile number must be 11 digits")]
        public string? MobileNumber { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password must have uppercase, lowercase, digit, and special character")]
        public string? Password { get; set; }
    }
}