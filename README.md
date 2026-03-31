# Philiprehberger.CreditCardValidator

[![CI](https://github.com/philiprehberger/dotnet-credit-card-validator/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-credit-card-validator/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.CreditCardValidator.svg)](https://www.nuget.org/packages/Philiprehberger.CreditCardValidator)
[![Last updated](https://img.shields.io/github/last-commit/philiprehberger/dotnet-credit-card-validator)](https://github.com/philiprehberger/dotnet-credit-card-validator/commits/main)

Validate credit card numbers with Luhn check, detect card brand, and mask for display.

## Installation

```bash
dotnet add package Philiprehberger.CreditCardValidator
```

## Usage

```csharp
using Philiprehberger.CreditCardValidator;

// Validate a card number
bool isValid = CreditCard.IsValid("4111111111111111"); // true

// Detect brand
CardBrand brand = CreditCard.DetectBrand("4111111111111111"); // Visa

// Mask for display
string masked = CreditCard.Mask("4111111111111111"); // "************1111"

// Format with spaces
string formatted = CreditCard.Format("4111111111111111"); // "4111 1111 1111 1111"

// Full validation
CardValidation result = CreditCard.Validate("4111111111111111");
// result.IsValid == true, result.Brand == CardBrand.Visa
```

## API

### `CreditCard`

| Method | Description |
|--------|-------------|
| `IsValid(string number)` | Returns `true` if the number passes the Luhn check. |
| `DetectBrand(string number)` | Returns the `CardBrand` based on the BIN prefix. |
| `Mask(string number, char maskChar = '*')` | Masks all but the last 4 digits. |
| `Format(string number)` | Formats the number with spaces in groups of 4. |
| `Validate(string number)` | Returns a `CardValidation` with validity, brand, and errors. |

### `CardBrand`

Enum: `Unknown`, `Visa`, `MasterCard`, `Amex`, `Discover`, `DinersClub`, `JCB`, `UnionPay`.

### `CardValidation`

Record: `IsValid` (bool), `Brand` (CardBrand), `Errors` (string[]).

### `LuhnValidator`

| Method | Description |
|--------|-------------|
| `IsValid(string digits)` | Luhn mod-10 check. Ignores spaces and dashes. |

### `BrandDetector`

| Method | Description |
|--------|-------------|
| `Detect(string number)` | Detects brand from BIN (first 6 digits). |

## Development

```bash
dotnet build src/Philiprehberger.CreditCardValidator.csproj --configuration Release
```

## Support

If you find this project useful:

⭐ [Star the repo](https://github.com/philiprehberger/dotnet-credit-card-validator)

🐛 [Report issues](https://github.com/philiprehberger/dotnet-credit-card-validator/issues?q=is%3Aissue+is%3Aopen+label%3Abug)

💡 [Suggest features](https://github.com/philiprehberger/dotnet-credit-card-validator/issues?q=is%3Aissue+is%3Aopen+label%3Aenhancement)

❤️ [Sponsor development](https://github.com/sponsors/philiprehberger)

🌐 [All Open Source Projects](https://philiprehberger.com/open-source-packages)

💻 [GitHub Profile](https://github.com/philiprehberger)

🔗 [LinkedIn Profile](https://www.linkedin.com/in/philiprehberger)

## License

[MIT](LICENSE)
