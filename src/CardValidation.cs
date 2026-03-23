namespace Philiprehberger.CreditCardValidator;

/// <summary>
/// Represents the result of a credit card validation.
/// </summary>
/// <param name="IsValid">Whether the card number is valid.</param>
/// <param name="Brand">The detected card brand.</param>
/// <param name="Errors">Any validation errors found.</param>
public record CardValidation(bool IsValid, CardBrand Brand, string[] Errors);
