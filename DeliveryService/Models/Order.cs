using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [Range(0.1, 100)]
    public double Weight { get; set; }

    [Required]
    public string District { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DeliveryTime { get; set; }
}