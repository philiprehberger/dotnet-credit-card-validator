namespace Philiprehberger.CreditCardValidator;

/// <summary>
/// Detects credit card brand from the BIN (Bank Identification Number) prefix.
/// </summary>
public static class BrandDetector
{
    /// <summary>
    /// Detects the card brand based on the first digits of the card number.
    /// </summary>
    /// <param name="number">The card number (spaces and dashes are ignored).</param>
    /// <returns>The detected <see cref="CardBrand"/>.</returns>
    public static CardBrand Detect(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            return CardBrand.Unknown;
        }

        var cleaned = LuhnValidator.CleanNumber(number);

        if (cleaned.Length < 2)
        {
            return CardBrand.Unknown;
        }

        // Amex: starts with 34 or 37
        if (cleaned.StartsWith("34") || cleaned.StartsWith("37"))
        {
            return CardBrand.Amex;
        }

        // JCB: 3528-3589
        if (cleaned.Length >= 4)
        {
            if (int.TryParse(cleaned[..4], out var jcbPrefix) && jcbPrefix >= 3528 && jcbPrefix <= 3589)
            {
                return CardBrand.JCB;
            }
        }

        // Diners Club: 300-305, 36, 38
        if (cleaned.StartsWith("36") || cleaned.StartsWith("38"))
        {
            return CardBrand.DinersClub;
        }

        if (cleaned.Length >= 3)
        {
            if (int.TryParse(cleaned[..3], out var dinersPrefix) && dinersPrefix >= 300 && dinersPrefix <= 305)
            {
                return CardBrand.DinersClub;
            }
        }

        // UnionPay: starts with 62
        if (cleaned.StartsWith("62"))
        {
            return CardBrand.UnionPay;
        }

        // Discover: 6011, 65, 644-649
        if (cleaned.StartsWith("6011") || cleaned.StartsWith("65"))
        {
            return CardBrand.Discover;
        }

        if (cleaned.Length >= 3)
        {
            if (int.TryParse(cleaned[..3], out var discoverPrefix) && discoverPrefix >= 644 && discoverPrefix <= 649)
            {
                return CardBrand.Discover;
            }
        }

        // MasterCard: 51-55 or 2221-2720
        if (cleaned.Length >= 2)
        {
            if (int.TryParse(cleaned[..2], out var mcPrefix2) && mcPrefix2 >= 51 && mcPrefix2 <= 55)
            {
                return CardBrand.MasterCard;
            }
        }

        if (cleaned.Length >= 4)
        {
            if (int.TryParse(cleaned[..4], out var mcPrefix4) && mcPrefix4 >= 2221 && mcPrefix4 <= 2720)
            {
                return CardBrand.MasterCard;
            }
        }

        // Visa: starts with 4
        if (cleaned.StartsWith('4'))
        {
            return CardBrand.Visa;
        }

        return CardBrand.Unknown;
    }
}
