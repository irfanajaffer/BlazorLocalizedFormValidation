using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Validation;

namespace BlazorLocalizedFormValidation.Shared;

[ValidatableType]
public sealed class ContactModel
{
    [Display(Name = nameof(ContactDisplayResources.Name), ResourceType = typeof(ContactDisplayResources))]
    [Required]
    public string? Name { get; set; }

    [Display(Name = nameof(ContactDisplayResources.Email), ResourceType = typeof(ContactDisplayResources))]
    [Required(ErrorMessage = "EmailRequired")]
    public string? Email { get; set; }
}
