using System.Globalization;
using System.Resources;
using BlazorLocalizedFormValidation.Shared;
using BlazorSSRSample.Validation;
using BlazorWasmSample.Client.Validation;

namespace BlazorWasmSample.Tests;

public sealed class SharedContactLocalizationTests
{
    private static readonly ResourceManager SsrResourceManager = new(
        "BlazorSSRSample.Resources.Validation.ContactValidationMessages",
        typeof(ContactValidationMessages).Assembly);

    private static readonly ResourceManager WasmResourceManager = new(
        "BlazorWasmSample.Client.Resources.Validation.ClientContactValidationMessages",
        typeof(ClientContactValidationMessages).Assembly);

    [Fact]
    public void French_resources_match_across_required_modes()
    {
        using var _ = new CultureScope("fr-FR");

        Assert.Equal(
            SsrResourceManager.GetString("ContactModel_Name_RequiredAttribute_Error", CultureInfo.CurrentUICulture),
            WasmResourceManager.GetString("ContactModel_Name_RequiredAttribute_Error", CultureInfo.CurrentUICulture));
        Assert.Equal(
            "FR convention membre : saisissez le nom.",
            SsrResourceManager.GetString("ContactModel_Name_RequiredAttribute_Error", CultureInfo.CurrentUICulture));

        Assert.Equal(
            SsrResourceManager.GetString("EmailRequired", CultureInfo.CurrentUICulture),
            WasmResourceManager.GetString("EmailRequired", CultureInfo.CurrentUICulture));
        Assert.Equal(
            "FR clé explicite : saisissez l’e-mail.",
            SsrResourceManager.GetString("EmailRequired", CultureInfo.CurrentUICulture));
        Assert.Equal("Nom", ContactDisplayResources.Name);
        Assert.Equal("E-mail", ContactDisplayResources.Email);
    }

    [Fact]
    public void German_resources_omit_the_conventional_name_key_in_both_required_modes()
    {
        using var _ = new CultureScope("de-DE");

        Assert.Null(SsrResourceManager.GetString("ContactModel_Name_RequiredAttribute_Error", CultureInfo.CurrentUICulture));
        Assert.Null(WasmResourceManager.GetString("ContactModel_Name_RequiredAttribute_Error", CultureInfo.CurrentUICulture));

        Assert.Equal(
            SsrResourceManager.GetString("EmailRequired", CultureInfo.CurrentUICulture),
            WasmResourceManager.GetString("EmailRequired", CultureInfo.CurrentUICulture));
        Assert.Equal(
            "DE expliziter Schlüssel: Geben Sie die E-Mail ein.",
            SsrResourceManager.GetString("EmailRequired", CultureInfo.CurrentUICulture));
        Assert.Equal("Name", ContactDisplayResources.Name);
        Assert.Equal("E-Mail", ContactDisplayResources.Email);
    }

    private sealed class CultureScope : IDisposable
    {
        private readonly CultureInfo originalCulture = CultureInfo.CurrentCulture;
        private readonly CultureInfo originalUICulture = CultureInfo.CurrentUICulture;

        public CultureScope(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        public void Dispose()
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUICulture;
        }
    }
}
