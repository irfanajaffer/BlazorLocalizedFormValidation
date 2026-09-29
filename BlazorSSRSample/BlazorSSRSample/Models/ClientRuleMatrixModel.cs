using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Validation;

namespace BlazorSSRSample.Models;

[ValidatableType]
public sealed class ClientRuleMatrixModel
{
    [Required(ErrorMessage = "RequiredMessage")]
    public string? RequiredText { get; set; }

    [StringLength(10, MinimumLength = 3, ErrorMessage = "StringLengthMessage")]
    public string? StringLengthText { get; set; }

    [MinLength(3, ErrorMessage = "MinLengthMessage")]
    public string? MinLengthText { get; set; }

    [MaxLength(5, ErrorMessage = "MaxLengthMessage")]
    public string? MaxLengthText { get; set; }

    [Range(1, 10, ErrorMessage = "RangeMessage")]
    public int RangeNumber { get; set; }

    [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "RegularExpressionMessage")]
    public string? RegularExpressionText { get; set; }

    [EmailAddress(ErrorMessage = "EmailAddressMessage")]
    public string? Email { get; set; }

    [Url(ErrorMessage = "UrlMessage")]
    public string? Url { get; set; }

    [Phone(ErrorMessage = "PhoneMessage")]
    public string? Phone { get; set; }

    [CreditCard(ErrorMessage = "CreditCardMessage")]
    public string? CreditCard { get; set; }

    public string? ComparisonSource { get; set; }

    [Compare(nameof(ComparisonSource), ErrorMessage = "CompareMessage")]
    public string? ComparisonValue { get; set; }

    [FileExtensions(Extensions = "pdf,txt", ErrorMessage = "FileExtensionsMessage")]
    public string? FileName { get; set; }

    [Required(ErrorMessage = "SelectionRequiredMessage")]
    public string? Selection { get; set; }
}

[ValidatableType]
public sealed class OptOutModel
{
    [Required(ErrorMessage = "OptOutRequiredMessage")]
    public string? Value { get; set; }
}
