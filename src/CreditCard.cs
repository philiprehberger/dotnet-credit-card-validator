namespace Philiprehberger.CreditCardValidator;

/// <summary>
/// Provides static methods for credit card validation, brand detection, masking, and formatting.
/// </summary>
public static class CreditCard
{
    /// <summary>
    /// Checks whether the given credit card number is valid using the Luhn algorithm.
    /// </summary>
    /// <param name="number">The card number to validate.</param>
    /// <returns><c>true</c> if the number passes the Luhn check; otherwise <c>false</c>.</returns>
    public static bool IsValid(string number)
    {
        return LuhnValidator.IsValid(number);
    }

    /// <summary>
    /// Detects the card brand from the card number prefix.
    /// </summary>
    /// <param name="number">The card number.</param>
    /// <returns>The detected <see cref="CardBrand"/>.</returns>
    public static CardBrand DetectBrand(string number)
    {
        return BrandDetector.Detect(number);
    }

    /// <summary>
    /// Masks the card number, showing only the last 4 digits.
    /// </summary>
    /// <param name="number">The card number to mask.</param>
    /// <param name="maskChar">The character to use for masking. Defaults to '*'.</param>
    /// <returns>The masked card number.</returns>
    /// <exception cref="ArgumentException">Thrown when the number has fewer than 4 digits.</exception>
    public static string Mask(string number, char maskChar = '*')
    {
        var cleaned = LuhnValidator.CleanNumber(number);

        if (cleaned.Length < 4)
        {
            throw new ArgumentException("Card number must have at least 4 digits.", nameof(number));
        }

        var lastFour = cleaned[^4..];
        var masked = new string(maskChar, cleaned.Length - 4);

        return masked + lastFour;
    }

    /// <summary>
    /// Formats the card number with spaces in groups of 4 digits.
    /// </summary>
    /// <param name="number">The card number to format.</param>
    /// <returns>The formatted card number.</returns>
    public static string Format(string number)
    {
        var cleaned = LuhnValidator.CleanNumber(number);
        var groups = new List<string>();

        for (var i = 0; i < cleaned.Length; i += 4)
        {
            var length = Math.Min(4, cleaned.Length - i);
            groups.Add(cleaned.Substring(i, length));
        }

        return string.Join(" ", groups);
    }

    /// <summary>
    /// Performs full validation on a card number, returning validity, brand, and any errors.
    /// </summary>
    /// <param name="number">The card number to validate.</param>
    /// <returns>A <see cref="CardValidation"/> with the results.</returns>
    public static CardValidation Validate(string number)
    {
        var errors = new List<string>();
        var cleaned = LuhnValidator.CleanNumber(number);

        if (string.IsNullOrWhiteSpace(cleaned))
        {
            errors.Add("Card number is empty.");
            return new CardValidation(false, CardBrand.Unknown, errors.ToArray());
        }

        if (!cleaned.All(char.IsDigit))
        {
            errors.Add("Card number contains non-digit characters.");
        }

        if (cleaned.Length < 12 || cleaned.Length > 19)
        {
            errors.Add("Card number length must be between 12 and 19 digits.");
        }

        if (!LuhnValidator.IsValid(cleaned))
        {
            errors.Add("Card number fails Luhn check.");
        }

        var brand = BrandDetector.Detect(cleaned);

        if (brand == CardBrand.Unknown)
        {
            errors.Add("Card brand could not be determined.");
        }

        var isValid = errors.Count == 0;

        return new CardValidation(isValid, brand, errors.ToArray());
    }
}
