using CommerceHub.Catalog.Domain.Common;
using CommerceHub.Catalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Catalog.Domain.Entities
{
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
    }
}
