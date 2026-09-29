using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Validation;

namespace BlazorWasmSample.Models;

[ValidatableType]
public sealed class ServerValidationModel
{
    [Display(Name = "NameDisplay")]
    [Required(ErrorMessage = "NameRequired")]
    public string? ExplicitName { get; set; }

    [Required]
    public string? ConventionalName { get; set; }

    [Display(Name = "CodeDisplay")]
    [StringLength(8, MinimumLength = 3)]
    public string? Code { get; set; }

    [Required(ErrorMessage = "ResourceKeyThatDoesNotExist")]
    public string? MissingResource { get; set; }

    public ServerAddress Address { get; set; } = new();

    public List<ServerContact> Contacts { get; set; } = new() { new ServerContact() };
}

public sealed class ServerAddress
{
    [Required(ErrorMessage = "StreetRequired")]
    public string? Street { get; set; }
}

public sealed class ServerContact
{
    [Required(ErrorMessage = "ContactEmailRequired")]
    [EmailAddress]
    public string? Email { get; set; }
}
