using PaymentStrategyPattern.Models;
using PaymentStrategyPattern.Models.SSLCommerz;
using PaymentStrategyPattern.Services;
using System.Text;
using System.Text.Json;

namespace PaymentStrategyPattern.Strategies;

public class SSLCommerzStrategy(IHttpClientFactory _httpClientFactory, ILogger<SSLCommerzStrategy> _logger) : IPaymentStrategy
{
    private readonly string store_id = "books687e26450e723";
    private readonly string storePassword = "books687e26450e723@ssl";

    private const string BASE_URL = "https://sandbox.sslcommerz.com/";
    private const string SUBMIT_URL = "gwprocess/v3/api.php";
    private const string VALIDATION_URL = "validator/api/validationserverAPI.php?wsdl";

    public string Name
    {
        get { return "SSLCommerz"; }
    }


    public async Task<string> PayAsync(decimal amount, string customerName)
    {


        var CustomerName = customerName as string;
        var CustomerEmail = "xyz@gmail.com";
        var CustomerPhone = "01645259878";
        var TotalPrice = amount.ToString();
        var CustomerAddress = "Mohakhali";

        var paymentData = new Dictionary<string, string>
        {
            ["store_id"] = store_id,
            ["store_passwd"] = storePassword,
            ["total_amount"] = amount.ToString(),
            ["currency"] = "BDT",
            ["tran_id"] = Guid.NewGuid().ToString(),
            ["success_url"] = "https://localhost:7180/checkout/success?gateway=SSLCommerz",
            ["fail_url"] = "https://localhost:7180/checkout/fail",
            ["cancel_url"] = "https://localhost:7180/Checkout/cancel",
            ["cus_name"] = customerName ?? "anonymous",
            ["cus_email"] = "xyz@gmail.com",
            ["cus_phone"] = "01645259878",
            ["cus_add1"] = "Mohakhali",
            ["cus_city"] = "Test City",
            ["cus_country"] = "Bangladesh",
            ["product_category"] = "Test",
            ["shipping_method"] = "NO",
            ["product_name"] = "Pizza",
            ["product_profile"] = "General",
            // Add other required fields as needed
        };


        //  var response = await _httpClientFactory.CreateClient().PostAsJsonAsync("https://sandbox.sslcommerz.com/gwprocess/v4/api.php", paymentData);
        // var responseString = await response.Content.ReadAsStringAsync();

        // var responseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);

        var client = _httpClientFactory.CreateClient();
        var content = new FormUrlEncodedContent(paymentData);
        var response = await client.PostAsync("https://sandbox.sslcommerz.com/gwprocess/v4/api.php", content);

        try
        {
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            if (result.TryGetProperty("GatewayPageURL", out var gatewayUrl))
            {
                return gatewayUrl.GetString();
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
        string? val_id = form["val_id"];
        if (string.IsNullOrEmpty(val_id))
        {
            return new PaymentResult
            {
                Success = false,
                Message = "Missing validation ID (val_id)."
            };
        }

        string url = $"https://sandbox.sslcommerz.com/validator/api/validationserverAPI.php?val_id={val_id}&store_id={store_id}&store_passwd={storePassword}&v=1&format=json";

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        try
        {
            var result = JsonSerializer.Deserialize<SSLCommerzResponse>(json);

            return new PaymentResult
            {
                Success = result?.status == "VALID",
                TransactionId = result?.tran_id,
                Amount = decimal.TryParse(result?.amount, out var amt) ? amt : 0,
                Message = result?.status ?? "Unknown"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing SSLCommerz verification response");
            return new PaymentResult
            {
                Success = false,
                Message = "Error parsing response"
            };
        }
    }
}