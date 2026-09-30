using System.ComponentModel.DataAnnotations;
using BlazorWasmSample.Client.Validation;
using Microsoft.Extensions.Validation;

namespace BlazorWasmSample.Client.Models;

[ValidatableType]
public sealed class ClientValidationModel
{
    [Display(Name = "NameDisplay")]
    [Required(ErrorMessage = "NameRequired")]
    public string? ExplicitName { get; set; }

    [Required]
    public string? ConventionalName { get; set; }

    [Display(Name = "CodeDisplay")]
    [StringLength(8, MinimumLength = 3)]
    public string? Code { get; set; }

    [FallbackRequired("ResourceKeyThatDoesNotExist")]
    public string? MissingResource { get; set; }

    public ClientAddress Address { get; set; } = new();

    public List<ClientContact> Contacts { get; set; } = [new()];
}

public sealed class ClientAddress
{
    [Required(ErrorMessage = "StreetRequired")]
    public string? Street { get; set; }
}

public sealed class ClientContact
{
    [Required(ErrorMessage = "ContactEmailRequired")]
    [EmailAddress]
    public string? Email { get; set; }
}
