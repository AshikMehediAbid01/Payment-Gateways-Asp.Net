namespace PaymentStrategyPattern.Models;

public class PaymentResponse
{
    public bool Success { get; init; }
    public string? TransactionId { get; init; }
    public string? ErrorMessage { get; init; }

    public static PaymentResponse Ok(string txnId) => new() { Success = true, TransactionId = txnId };
    public static PaymentResponse Fail(string error) => new() { Success = false, ErrorMessage = error };
}