using PaymentStrategyPattern.Models;

namespace PaymentStrategyPattern.Services;

public interface IPaymentStrategy
{
    string Name { get; }
    Task<string> PayAsync(decimal amount, string customerName);
    Task<PaymentResult> VerifyPaymentAsync(IQueryCollection query, IFormCollection form);
}