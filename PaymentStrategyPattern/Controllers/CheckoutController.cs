using Microsoft.AspNetCore.Mvc;
using PaymentStrategyPattern.Models;
using PaymentStrategyPattern.Services;

namespace PaymentStrategyPattern.Controllers;

public class CheckoutController : Controller
{
    private readonly PaymentService _paymentService;
    private readonly ILogger<CheckoutController> _logger;
    public CheckoutController(PaymentService paymentService, ILogger<CheckoutController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    
    {
        ViewBag.Gateways = new List<string> { "AamarPay", "SSLCommerz" };
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment(string gateway, OrderDetails orderDetails)
    {
        var paymentUrl = await _paymentService.ProcessPaymentAsync(gateway, orderDetails);
        ViewBag.Result = paymentUrl;
        if (paymentUrl.StartsWith("http"))
            return Redirect(paymentUrl); // controller decides redirection
        else
            return BadRequest(paymentUrl); // controller handles error
    }


    [HttpPost]
    public async Task<IActionResult> Success([FromQuery] string gateway)
   {
        var result = await _paymentService.VerifyPaymentAsync(gateway, Request.Query, Request.Form);
        if (result.Success)
        {
            _logger.LogInformation("✅ {Gateway} payment success for {Txn}", gateway, result.TransactionId);
            return View("Success", result);
        }
        return View("Fail", result);
    }

    [HttpPost]
    public IActionResult Fail()
    {
        return View("Fail");
    }

    [HttpPost("cancel")]
    public IActionResult Cancel([FromForm] string gateway)
    {
        _logger.LogInformation("⚠️ {Gateway} payment cancelled", gateway);
        return View("Cancel");
    }
}