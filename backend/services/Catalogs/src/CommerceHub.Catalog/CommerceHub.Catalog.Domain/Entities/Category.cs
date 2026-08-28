using CommerceHub.Catalog.Domain.Common;
using CommerceHub.Catalog.Domain.Enums;
using CommerceHub.Catalog.Domain.Exceptions;

namespace CommerceHub.Catalog.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; private set; } =string.Empty;

        private Category()
        {
        }

        public static Category Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name is required.");

            return new Category
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                CreatedOnUtc = DateTime.UtcNow
            };
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name is required.");

            Name = name.Trim();
            UpdatedOnUtc = DateTime.UtcNow;
        }
    }
}
