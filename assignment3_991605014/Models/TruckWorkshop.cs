using System.ComponentModel.DataAnnotations;

namespace assignment3_991605014.Models
{
    public class TruckWorkshop
    {
        [Key]
        public int WorkOrderID { get; set; }

        [Required]
        public string workDescription { get; set; }

        [Required]
        public decimal cost { get; set; }

        [Required]
        public int TruckID { get; set; }
    }
}
