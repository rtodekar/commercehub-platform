using CommerceHub.Catalog.Domain.Common;
using CommerceHub.Catalog.Domain.Enums;
using CommerceHub.Catalog.Domain.Exceptions;

namespace CommerceHub.Catalog.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int StockQuantity { get; private set; }

    public ProductStatus Status { get; private set; }

    public Guid CategoryId { get; private set; }

    private Product()
    {
    }

    public static Product Create(
        string name,
        decimal price,
        int stockQuantity,
        Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required.");

        if (price <= 0)
            throw new DomainException("Product price must be greater than zero.");

        if (stockQuantity < 0)
            throw new DomainException("Stock quantity cannot be negative.");

        if (categoryId == Guid.Empty)
            throw new DomainException("Category is required.");

        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Price = price,
            StockQuantity = stockQuantity,
            CategoryId = categoryId,
            Status = ProductStatus.Draft,
            CreatedOnUtc = DateTime.UtcNow
        };
    }

    public void UpdatePrice(decimal price)
    {
        if (price <= 0)
            throw new DomainException("Product price must be greater than zero.");

        Price = price;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

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
        Status = ProductStatus.Inactive;
        UpdatedOnUtc = DateTime.UtcNow;
    }
}