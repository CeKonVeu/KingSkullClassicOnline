using KingSkullClassicOnline.Client;
using KingSkullClassicOnline.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddScoped<Data>();

if (builder.HostEnvironment.IsDevelopment())
    builder.Logging.SetMinimumLevel(LogLevel.Information);

await builder.Build().RunAsync();