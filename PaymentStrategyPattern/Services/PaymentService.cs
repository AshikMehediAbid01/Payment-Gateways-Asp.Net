using PaymentStrategyPattern.Models;

namespace PaymentStrategyPattern.Services;

public class PaymentService
{
    private readonly PaymentStrategyFactory _factory;

    public PaymentService(PaymentStrategyFactory factory)
    {
        _factory = factory;
    }

    public Task<string> ProcessPaymentAsync(string gateway, decimal amount, string customerName)
    {
        var strategy = _factory.GetStrategy(gateway);
        return strategy.PayAsync(amount, customerName);
    }

    public Task<PaymentResult> VerifyPaymentAsync(string gateway, IQueryCollection query, IFormCollection form)
    {
        var strategy = _factory.GetStrategy(gateway);
        return strategy.VerifyPaymentAsync(query, form);
    }
}