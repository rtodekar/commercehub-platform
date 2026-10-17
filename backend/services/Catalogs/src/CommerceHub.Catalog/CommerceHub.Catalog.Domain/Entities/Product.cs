using CommerceHub.Catalog.Domain.Common;
using CommerceHub.Catalog.Domain.Enums;
using CommerceHub.Catalog.Domain.Exceptions;
using CommerceHub.Catalog.Domain.ValueObjects;

namespace CommerceHub.Catalog.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public Sku Sku { get; private set; } = null!;

    public Money Price { get; private set; } = null!;

    public int StockQuantity { get; private set; }

    public ProductStatus Status { get; private set; }

    public Guid CategoryId { get; private set; }

    private Product()
    {
    }

    public static Product Create(
        string name,
        string? description,
        string sku,
        decimal price,
        int stockQuantity,
        Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required.");

        if (name.Trim().Length > 200)
            throw new DomainException(
                "Product name cannot exceed 200 characters.");

        description = description?.Trim() ?? string.Empty;

        if (description.Length > 2000)
            throw new DomainException(
                "Product description cannot exceed 2000 characters.");

        if (price <= 0)
            throw new DomainException(
                "Product price must be greater than zero.");

        if (stockQuantity < 0)
            throw new DomainException(
                "Stock quantity cannot be negative.");

        if (categoryId == Guid.Empty)
            throw new DomainException(
                "Category is required.");

        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description,
            Sku = Sku.Create(sku),
            Price = Money.Create(price),
            StockQuantity = stockQuantity,
            CategoryId = categoryId,
            Status = ProductStatus.Draft,
            CreatedOnUtc = DateTime.UtcNow
        };
    }

    public void UpdatePrice(decimal price)
    {
        if (price <= 0)
            throw new DomainException(
                "Product price must be greater than zero.");

        Price = Money.Create(price);
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required.");

        name = name.Trim();

        if (name.Length > 200)
            throw new DomainException(
                "Product name cannot exceed 200 characters.");

        Name = name;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        if (quantity > int.MaxValue - StockQuantity)
            throw new DomainException(
                "Stock quantity exceeds the maximum allowed value.");

        StockQuantity += quantity;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        if (quantity > StockQuantity)
            throw new DomainException("Insufficient stock available.");

        StockQuantity -= quantity;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (Status == ProductStatus.Discontinued)
            throw new DomainException(
                "Discontinued product cannot be activated.");

        Status = ProductStatus.Active;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (Status == ProductStatus.Discontinued)
            throw new DomainException(
                "Discontinued product cannot be deactivated.");

        Status = ProductStatus.Inactive;
        UpdatedOnUtc = DateTime.UtcNow;
    }
}