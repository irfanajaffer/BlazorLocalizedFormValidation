using System.ComponentModel.DataAnnotations;
using BlazorSSRSample.Validation;
using Microsoft.Extensions.Validation;

namespace BlazorSSRSample.Models;

[ValidatableType]
public sealed class ValidationLabModel
{
    [Display(Name = "NameDisplay")]
    [Required(ErrorMessage = "NameRequired")]
    public string? ExplicitName { get; set; }

    [Required]
    public string? ConventionalName { get; set; }

    [Required]
    public string? TypeConvention { get; set; }

    public GlobalConventionModel GlobalConvention { get; set; } = new();

    [Display(Name = "CodeDisplay")]
    [StringLength(8, MinimumLength = 3)]
    public string? Code { get; set; }

    [Required(ErrorMessage = "ResourceKeyThatDoesNotExist")]
    public string? MissingResource { get; set; }

    [Display(
        Name = nameof(AttributeValidationResources.LegacyNameDisplay),
        ResourceType = typeof(AttributeValidationResources))]
    [Required(
        ErrorMessageResourceName = nameof(AttributeValidationResources.LegacyNameRequired),
        ErrorMessageResourceType = typeof(AttributeValidationResources))]
    public string? AttributeOwned { get; set; }

    public AddressModel Address { get; set; } = new();

    public List<ContactModel> Contacts { get; set; } = [new()];

    public SharedValidationModel Shared { get; set; } = new();
}

public sealed class AddressModel
{
    [Required(ErrorMessage = "StreetRequired")]
    public string? Street { get; set; }
}

public sealed class ContactModel
{
    [Required(ErrorMessage = "ContactEmailRequired")]
    [EmailAddress]
    public string? Email { get; set; }
}

public sealed class SharedValidationModel
{
    [Required(ErrorMessage = "SharedValueRequired")]
    public string? Value { get; set; }
}

public sealed class GlobalConventionModel
{
    [Required]
    public string? Value { get; set; }
}
