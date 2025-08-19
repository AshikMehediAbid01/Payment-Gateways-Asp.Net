using System.ComponentModel.DataAnnotations;

namespace PaymentStrategyPattern.Models;

public class PaymentRequest
{
    [Range(1, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public string Currency { get; set; } = "BDT";

    [Required]
    public string CustomerName { get; set; } = default!;

    [Required]
    public string CustomerPhone { get; set; } = default!;

    [EmailAddress]
    public string? CustomerEmail { get; set; }

    public string? Description { get; set; }
}