using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRegistrationSystemWebAPI.Models
{
    public class Registration
    {
        [Key, ForeignKey("Vehicle")] // Link Vehicle Id as (PK, FK) define as one to one relation.
        public Guid VehicleId { get; set; }


        [Required, RegularExpression(@"^(N\/D|[A-Z]{3}-\d{4})$", 
            ErrorMessage = "Must be in format ABC-1234")]
        public string PlateNumber { get; set; } = "N/D"; // e.g., ABC-1234, XYZ-5678


        [Required]
        public DateOnly IssueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now); // e.g., 2023-01-015


        [Required, StringLength(35), RegularExpression(@"^(N\/D|[A-Za-z\s]+)$", 
            ErrorMessage = "Must only contain letters")]
        public string RegistrationCity { get; set; } = "N/D"; // e.g., Karachi, Lahore, Islamabad


        [Required, StringLength(40), RegularExpression(@"^(N\/D|[A-Za-z\s]+)$", 
            ErrorMessage = "Must only contain letters and spaces")]
        public string OwnerName { get; set; } = "N/D"; // e.g., John Doe, Jane Smith


        [Required, RegularExpression(@"^(N\/D|\d{5}-\d{7}-\d)$", 
            ErrorMessage = "Must be in format 12345-6789012-3")]
        public string OwnerCNIC { get; set; } = "N/D"; // e.g., 12345-6789012-3
    }
}
