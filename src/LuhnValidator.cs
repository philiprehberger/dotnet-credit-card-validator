namespace Philiprehberger.CreditCardValidator;

/// <summary>
/// Validates numbers using the Luhn mod-10 algorithm.
/// </summary>
public static class LuhnValidator
{
    /// <summary>
    /// Checks whether the given digit string passes the Luhn algorithm.
    /// Spaces and dashes are ignored.
    /// </summary>
    /// <param name="digits">The digit string to validate.</param>
    /// <returns><c>true</c> if the number passes the Luhn check; otherwise <c>false</c>.</returns>
    public static bool IsValid(string digits)
    {
        if (string.IsNullOrWhiteSpace(digits))
        {
            return false;
        }

        var cleaned = CleanNumber(digits);

        if (cleaned.Length < 2)
        {
            return false;
        }

        var sum = 0;
        var alternate = false;

        for (var i = cleaned.Length - 1; i >= 0; i--)
        {
            var c = cleaned[i];

            if (!char.IsDigit(c))
            {
                return false;
            }

            var n = c - '0';

            if (alternate)
            {
                n *= 2;
                if (n > 9)
                {
                    n -= 9;
                }
            }

            sum += n;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }

    /// <summary>
    /// Removes spaces and dashes from a card number string.
    /// </summary>
    /// <param name="number">The raw card number.</param>
    /// <returns>The cleaned digit string.</returns>
    internal static string CleanNumber(string number)
    {
        return number.Replace(" ", "").Replace("-", "");
    }
}
