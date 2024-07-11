using System.ComponentModel.DataAnnotations;

namespace assignment3_991605014.Models
{
    public class Truck
    {
        [Key]
        public int TruckId { get; set; }

        [Required(ErrorMessage = "Truck number is required.")]
        [StringLength(10, ErrorMessage = "Truck number cannot be longer than 10 characters.")]
        public string TruckNum { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(20, ErrorMessage = "Model name cannot be longer than 20 characters.")]
        public string TModel { get; set; }

        [Required(ErrorMessage = "Make is required.")]
        [StringLength(10, ErrorMessage = "Make name cannot be longer than 10 characters.")]
        public string TMake { get; set; }
    }
}