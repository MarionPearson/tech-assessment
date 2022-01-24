using System.ComponentModel.DataAnnotations;

namespace SogetiAssessment.Models
{
    public class Order
    {
        [Key, Required]
        public int OrderId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}