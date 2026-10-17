using CommerceHub.Catalog.Domain.Common;
using CommerceHub.Catalog.Domain.Exceptions;

namespace CommerceHub.Catalog.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        private Category()
        {
        }

        public static Category Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name is required.");

            name = name.Trim();

            if (name.Length > 200)
                throw new DomainException(
                    "Category name cannot exceed 200 characters.");

            return new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedOnUtc = DateTime.UtcNow
            };
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name is required.");

            name = name.Trim();

            if (name.Length > 200)
                throw new DomainException(
                    "Category name cannot exceed 200 characters.");

            Name = name;
            UpdatedOnUtc = DateTime.UtcNow;
        }
    }
}
