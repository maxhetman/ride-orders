using System.ComponentModel.DataAnnotations;

namespace Orders.Models
{
    public class Order
    {
        public long Id { get; set; }

        [Display(Name= "Pickup location")]
        [StringLength(120, MinimumLength = 5)]
        [Required]
        public string AddressFrom { get; set; }

        [Display(Name = "Destination")]
        [StringLength(120, MinimumLength = 5)]
        [Required]
        public string AddressTo { get; set; }

        [StringLength(12, MinimumLength = 3)]
        [RegularExpression(@"^[0-9]*$")]
        public string Phone { get; set; }

        [Range(1, 100)]
        public int Price { get; set; }

        public bool Cancelled { get; set; }

    }
}
