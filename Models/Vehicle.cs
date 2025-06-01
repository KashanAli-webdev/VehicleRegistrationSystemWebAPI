using System.ComponentModel.DataAnnotations;
using VehicleRegistrationSystemWebAPI.Utilities;

namespace VehicleRegistrationSystemWebAPI.Models
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }


        [Required, StringLength(30, ErrorMessage = "Can't exceed 30 characters"), 
            RegularExpression(@"^[A-Za-z0-9\s\-]+$", ErrorMessage = "Must be alphanumeric")]
        public string Brand { get; set; } = null!; // e.g., Toyota, Honda, Ford


        [Required, StringLength(30, ErrorMessage = "Can't exceed 30 characters"), 
            RegularExpression(@"^[A-Za-z0-9\s\-]+$", ErrorMessage = "Must be alphanumeric")]
        public string Model { get; set; } = null!; // e.g., Corolla, Civic, F-150


        [Required, StringLength(20, ErrorMessage = "Can't exceed 20 characters"),
            RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Must only contain letters")]
        public string Color { get; set; } = null!; // e.g., Red, Blue, Black


        [Required]
        public VehicleType Type { get; set; } // e.g., Sedan, SUV, Truck


        [Required, Range(1886, 2100, ErrorMessage = "Must be between 1886 - 2100")]
        public ushort Year { get; set; } // e.g., 2023, 2024, 2025


        [Required, Range(50, 18000, ErrorMessage = "EngineCC must be between 50 and 10000")]
        public ushort EngineCC { get; set; } // e.g., 1500, 2000, 2500


        [Required]
        public Registration Registration { get; set; } = null!; // One-to-One Navigation
    }
}
