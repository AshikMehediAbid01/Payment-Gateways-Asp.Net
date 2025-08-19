using PaymentStrategyPattern.Services;
using PaymentStrategyPattern.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient factory
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register strategies (each implements IPaymentStrategy with a unique Key)
builder.Services.AddScoped<IPaymentStrategy, AamarPayStrategy>();
builder.Services.AddScoped<IPaymentStrategy, SSLCommerzStrategy>();


// Factory that gathers all strategies and lets us resolve by key at runtime
builder.Services.AddScoped<PaymentStrategyFactory>();


// High-level payment service used by controllers
builder.Services.AddScoped<PaymentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
