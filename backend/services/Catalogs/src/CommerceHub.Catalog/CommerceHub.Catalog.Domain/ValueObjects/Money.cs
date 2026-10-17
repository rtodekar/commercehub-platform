using CommerceHub.Catalog.Domain.Exceptions;

namespace CommerceHub.Catalog.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }

    private Money()
    {
    }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new DomainException("Amount cannot be negative.");

        Amount = amount;
    }

    public static Money Create(decimal amount)
    {
        return new Money(amount);
    }

    public override string ToString()
    {
        return Amount.ToString("0.00");
    }
}