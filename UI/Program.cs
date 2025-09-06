using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UI;
using UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseAddress = builder.HostEnvironment.IsDevelopment()
    ? "https://localhost:7054/"
    : "";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

await builder.Build().RunAsync();
