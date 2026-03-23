namespace Philiprehberger.CreditCardValidator;

/// <summary>
/// Represents known credit card brands.
/// </summary>
public enum CardBrand
{
    /// <summary>Unknown or unrecognized card brand.</summary>
    Unknown = 0,

    /// <summary>Visa (prefix: 4).</summary>
    Visa,

    /// <summary>MasterCard (prefix: 51-55, 2221-2720).</summary>
    MasterCard,

    /// <summary>American Express (prefix: 34, 37).</summary>
    Amex,

    /// <summary>Discover (prefix: 6011, 65, 644-649).</summary>
    Discover,

    /// <summary>Diners Club (prefix: 300-305, 36, 38).</summary>
    DinersClub,

    /// <summary>JCB (prefix: 3528-3589).</summary>
    JCB,

    /// <summary>UnionPay (prefix: 62).</summary>
    UnionPay
}
