namespace PaymentStrategyPattern.Models.AamarPay;

public class AamarPayValidationResponse
{
    public string pg_txnid { get; set; }
    public string mer_txnid { get; set; }
    public string pay_status { get; set; }
    public string status_title { get; set; }
    public string amount { get; set; }
    public string currency { get; set; }
    public string payment_processor { get; set; }
    public string bank_trxid { get; set; }
    public string approval_code { get; set; }
    public string cus_name { get; set; }
    public string cus_email { get; set; }
    public string cus_phone { get; set; }
    public string desc { get; set; }
    public string date { get; set; }
    public string ip { get; set; }
    public string store_amount { get; set; }
}
