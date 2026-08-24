using CommerceHub.Catalog.Domain.Common;

namespace CommerceHub.Catalog.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; private set; } =string.Empty;
        private Category()
        {
        }
    }
}
