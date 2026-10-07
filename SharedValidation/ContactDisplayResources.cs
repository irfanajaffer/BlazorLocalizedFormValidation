using System.Globalization;
using System.Resources;

namespace BlazorLocalizedFormValidation.Shared;

public static class ContactDisplayResources
{
    private static readonly ResourceManager ResourceManager = new(
        "BlazorLocalizedFormValidation.Shared.Resources.ContactDisplayResources",
        typeof(ContactDisplayResources).Assembly);

    public static string Name =>
        ResourceManager.GetString(nameof(Name), CultureInfo.CurrentUICulture)!;

    public static string Email =>
        ResourceManager.GetString(nameof(Email), CultureInfo.CurrentUICulture)!;
}
