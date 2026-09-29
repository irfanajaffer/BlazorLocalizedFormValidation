using System.Globalization;
using BlazorWasmSample.Client.Pages;
using BlazorWasmSample.Client.Validation;
using BlazorWasmSample.Components;
using BlazorWasmSample.Validation;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddClientValidation();
builder.Services.AddValidation(options =>
{
    options.LocalizerProvider = (type, factory) =>
        type.Namespace?.StartsWith("BlazorWasmSample.Client", StringComparison.Ordinal) == true
            ? factory.Create(typeof(ClientValidationMessages))
            : factory.Create(typeof(ServerValidationMessages));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// The query-string culture providers only apply to the initial document
// request. An Interactive Server render mode opens a *separate* SignalR
// negotiate/connect request that carries no query string, so the circuit
// falls through to whatever provider runs next. A cookie is the documented
// mechanism to make the selected culture available to that follow-up
// request too, so the circuit boots with the correct culture.
var supportedCultures = new[] { "fr-FR", "de-DE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localizationOptions.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
app.UseRequestLocalization(localizationOptions);

// Sets the culture cookie and redirects back to the originating page with a
// full page load, so the culture is already resolved from the cookie before
// the Interactive Server circuit's SignalR handshake happens.
app.MapGet("/set-culture", (HttpContext context, string culture, string redirectUri) =>
{
    context.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true });

    return Results.LocalRedirect(redirectUri);
});

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWasmSample.Client._Imports).Assembly);

app.Run();
