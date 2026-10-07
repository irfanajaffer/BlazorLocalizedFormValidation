using System.ComponentModel.DataAnnotations;
using System.Globalization;
using BlazorWasmSample.Client.Validation;

namespace BlazorWasmSample.Tests;

public sealed class FallbackRequiredAttributeTests
{
    [Fact]
    public void FormatErrorMessage_returns_the_framework_required_message_when_the_resource_key_is_unresolved()
    {
        using var _ = new CultureScope("fr-FR");
        var attribute = new FallbackRequiredAttribute("ResourceKeyThatDoesNotExist");

        var actual = attribute.FormatErrorMessage("Missing resource");
        var expected = new RequiredAttribute().FormatErrorMessage("Missing resource");

        Assert.Equal(expected, actual);
        Assert.NotEqual("ResourceKeyThatDoesNotExist", actual);
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
