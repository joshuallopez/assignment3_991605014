using System.ComponentModel.DataAnnotations;

namespace assignment3_991605014.Models
{
    public class TransportRoute
    {
        [Key]
        public int RouteId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RouteNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string RouteName { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Length must be greater than 0.")]
        public double RLength { get; set; }

        [Required]
        [Range(0.01, 100, ErrorMessage = "Pay per km must be greater than 0 and less than or equal to 100.")]
        public decimal RPayPerKm { get; set; }
    }
}