using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScreenSound.WebAssembly;
using ScreenSound.WebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient<ArtistasAPI>();
builder.Services.AddTransient<MusicasAPI>();

builder.Services.AddHttpClient(
    "API",
    client =>
    {
        string? apiUrl = builder.Configuration["APIServer"];
        if (string.IsNullOrEmpty(apiUrl))
        {
            throw new NullReferenceException("Uri on Appsettings.json (APIServer) is enpity!");
        }

        client.BaseAddress = new Uri(apiUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }
);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});

await builder.Build().RunAsync();
