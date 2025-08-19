using PaymentStrategyPattern.Models;

namespace PaymentStrategyPattern.Services;

public interface IPaymentStrategy
{
    string Name { get; }
    Task<string> PayAsync(OrderDetails orderDetails);
    Task<PaymentResult> VerifyPaymentAsync(IQueryCollection query, IFormCollection form);
}