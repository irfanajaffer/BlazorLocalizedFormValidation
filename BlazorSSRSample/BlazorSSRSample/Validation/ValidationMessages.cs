using System.Globalization;
using System.Resources;

namespace BlazorSSRSample.Validation;

public sealed class ValidationMessages;

public static class AttributeValidationResources
{
    private static readonly ResourceManager ResourceManager = new(
        "BlazorSSRSample.Resources.AttributeValidationResources",
        typeof(AttributeValidationResources).Assembly);

    public static string LegacyNameDisplay =>
        ResourceManager.GetString(
            nameof(LegacyNameDisplay),
            CultureInfo.CurrentUICulture)!;

    public static string LegacyNameRequired =>
        ResourceManager.GetString(
            nameof(LegacyNameRequired),
            CultureInfo.CurrentUICulture)!;
}
