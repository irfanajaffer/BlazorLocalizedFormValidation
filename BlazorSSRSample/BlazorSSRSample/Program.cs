using BlazorSSRSample.Components;
using BlazorSSRSample.Models;
using BlazorSSRSample.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents(options =>
{
    options.DisableClientValidation =
        builder.Configuration.GetValue<bool>("Validation:DisableClientValidation");
});
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddValidation(options =>
{
    options.LocalizerProvider = (type, factory) =>
        type == typeof(SharedValidationModel) ||
        type == typeof(AddressModel) ||
        type == typeof(ContactModel)
            ? factory.Create(typeof(ValidationMessages))
            : factory.Create(type);
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
app.UseRequestLocalization(["fr-FR", "de-DE"]);

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.Run();
