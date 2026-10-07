using System.ComponentModel.DataAnnotations;

namespace BlazorWasmSample.Client.Validation;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
public sealed class FallbackRequiredAttribute(string resourceKey) : RequiredAttribute
{
    public string ResourceKey { get; } = resourceKey ?? throw new ArgumentNullException(nameof(resourceKey));

    public override string FormatErrorMessage(string name)
    {
        var localizedMessage = base.FormatErrorMessage(name);

        return string.Equals(localizedMessage, ResourceKey, StringComparison.Ordinal)
            ? new RequiredAttribute().FormatErrorMessage(name)
            : localizedMessage;
    }
}
