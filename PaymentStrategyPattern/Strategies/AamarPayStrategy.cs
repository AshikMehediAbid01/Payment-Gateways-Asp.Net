using PaymentStrategyPattern.Models;
using PaymentStrategyPattern.Models.AamarPay;
using PaymentStrategyPattern.Services;
using System.Text.Json;

namespace PaymentStrategyPattern.Strategies;

public class AamarPayStrategy(IHttpClientFactory _httpClientFactory, ILogger<AamarPayStrategy> _logger) : IPaymentStrategy
{
    private readonly string storeId = "aamarpaytest";
    private readonly string signatureKey = "dbb74894e82415a2f7ff0ec3a97e4183";

    private const string SandboxApiUrl = "https://sandbox.aamarpay.com/jsonpost.php";

    public string Name
    {
        get { return "AamarPay"; }
    }


    public async Task<string> PayAsync(decimal amount, string customerName)
    {


        var CustomerName = customerName as string;
        var CustomerEmail = "xyz@gmail.com";
        var CustomerPhone = "01645259878";
        var TotalPrice = amount.ToString();
        var CustomerAddress = "Mohakhali";

        var paymentData = new AamarPayRequest
        {
            store_id = storeId,
            tran_id = Guid.NewGuid().ToString(),
            //success_url = "https://localhost:7180/checkout/success",
            success_url = "https://localhost:7180/checkout/success?gateway=AamarPay",
            fail_url = "https://localhost:7180/checkout/fail",
            cancel_url = "https://localhost:7180/Checkout/cancel",
            amount = TotalPrice,
            currency = "BDT",
            signature_key = signatureKey,
            desc = "Merchant Registration Payment",
            cus_name = CustomerName ?? "anonymous",
            cus_email = CustomerEmail ?? "payer@merchantcusomter.com",
            cus_add1 = CustomerAddress,
            cus_add2 = "Mohakhali DOHS",
            cus_city = "Dhaka",
            cus_state = "Dhaka",
            cus_postcode = "1206",
            cus_country = "Bangladesh",
            cus_phone = CustomerPhone,
            type = "json"
        };


        var response = await _httpClientFactory.CreateClient().PostAsJsonAsync(SandboxApiUrl, paymentData);
        var responseString = await response.Content.ReadAsStringAsync();
        // var responseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);

        try
        {
            var responseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
            if (responseData.TryGetValue("payment_url", out var paymentUrl))
            {
                return paymentUrl;
            }
        }
        catch (Exception)
        {
            return "Something went wrong";
        }
        return "Payment URL not found in response.";
    }

    public async Task<PaymentResult> VerifyPaymentAsync(IQueryCollection query, IFormCollection form)
    {
        string? mer_txnid = form["mer_txnid"];
        if (string.IsNullOrEmpty(mer_txnid))
        {
            return new PaymentResult
            {
                Success = false,
                Message = "Invalid callback: missing transaction id",
            };
        }

        string url = $"https://sandbox.aamarpay.com/api/v1/trxcheck/request.php?request_id={mer_txnid}&store_id={storeId}&signature_key={signatureKey}&type=json";

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        try
        {
            var result = JsonSerializer.Deserialize<AamarPayValidationResponse>(json);

            return new PaymentResult
            {
                Success = result?.pay_status == "Successful",
                TransactionId = result?.pg_txnid ?? mer_txnid,
                Amount = decimal.TryParse(result?.amount, out var amt) ? amt : 0,
                Message = result?.pay_status ?? "Unknown"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying payment");
            return new PaymentResult
            {
                Success = false,
                Message = "Error parsing verification response"
            };
        }

    }
}
