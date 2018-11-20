using System.ComponentModel.DataAnnotations;

namespace Orders.Models
{
    public class CreateOrderViewModel
    {
        [StringLength(120, MinimumLength = 5)]
        [Required]
        public string AddressFrom { get; set; }

        [StringLength(120, MinimumLength = 5)]
        [Required]
        public string AddressTo { get; set; }

        [StringLength(12, MinimumLength = 3)]
        [RegularExpression(@"^[0-9]*$")]
        public string Phone { get; set; }
    }
}
