using System;
using System.Collections.Generic;
using System.Text;

namespace Torion.Domain.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
