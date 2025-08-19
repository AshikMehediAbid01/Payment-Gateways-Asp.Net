using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaymentStrategyPattern.Models.SSLCommerz;
public class SSLCommerzRequest
{
    [JsonPropertyName("store_id")]
    public string store_id { get; set; }
    [JsonPropertyName("store_passwd")]
    public string store_passwd { get; set; }
    public string total_amount { get; set; }
    public string currency { get; set; } = "BDT";
    public string tran_id { get; set; }
    public string product_category { get; set; } = "Test";
    public string success_url { get; set; }
    public string fail_url { get; set; }
    public string cancel_url { get; set; }
    public string cus_name { get; set; }
    public string cus_city { get; set; } = "Test City";
    public string cus_email { get; set; }
    public string cus_phone { get; set; }
    public string cus_add1 { get; set; }
}
