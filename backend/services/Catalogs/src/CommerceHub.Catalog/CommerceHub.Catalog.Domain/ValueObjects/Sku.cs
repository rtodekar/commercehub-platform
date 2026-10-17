using CommerceHub.Catalog.Domain.Exceptions;

namespace CommerceHub.Catalog.Domain.ValueObjects;

public sealed record Sku
{
    public string Value { get; } = string.Empty;

    private Sku()
    {
    }

    public Sku(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("SKU is required.");

        value = value.Trim();

        if (value.Length > 64)
            throw new DomainException(
                "SKU cannot exceed 64 characters.");

        Value = value.ToUpperInvariant();
    }

    public static Sku Create(string value)
    {
        return new Sku(value);
    }

    public override string ToString()
    {
        return Value;
    }
}