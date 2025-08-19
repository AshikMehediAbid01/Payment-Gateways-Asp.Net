
namespace PaymentStrategyPattern.Services;

public class PaymentStrategyFactory
{
    // This will hold all available payment strategies that implement IPaymentStrategy.
    // Example: AamarPayStrategy, SSLCommerzStrategy, StripeStrategy, etc.
    // IEnumerable<IPaymentStrategy> = a collection of all payment strategy implementations (e.g. AamarPayStrategy, SSLCommerzStrategy).

    private readonly IEnumerable<IPaymentStrategy> _strategies;


    /// <summary>
    /// Constructor where ASP.NET Core Dependency Injection (DI) will
    /// automatically inject ALL registered strategies.
    /// </summary>
    /// <param name="strategies">Collection of all strategies from DI</param>
    public PaymentStrategyFactory(IEnumerable<IPaymentStrategy> strategies)
    {
        // Save the injected strategies for later use
        _strategies = strategies;
    }

    public IPaymentStrategy GetStrategy(string gatewayName)
    {
        return _strategies.FirstOrDefault(s => s.Name.Equals(gatewayName, StringComparison.OrdinalIgnoreCase))
               ?? throw new Exception($"Payment strategy '{gatewayName}' not found.");
    }
}