using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Catalog.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedOnUtc { get; protected set; }
        public DateTime? UpdatedOnUtc { get; protected set; }
    }
}
