using BlazorWasmSample.Client.Validation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// Culture is now resolved and applied before the WebAssembly runtime boots via
// Blazor.start({ webAssembly: { applicationCulture } }) in App.razor, which sets
// CultureInfo.DefaultThreadCurrentCulture/CurrentUICulture as part of startup.
// Setting culture here (after host.Build()) would run too late and cause a
// render-with-default-culture-then-rerender flicker.
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddClientValidation();

var host = builder.Build();

await host.RunAsync();
