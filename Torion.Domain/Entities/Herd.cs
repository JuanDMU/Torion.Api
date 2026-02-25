using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Base;
using Torion.Domain.Enumerations;

namespace Torion.Domain.Entities
{
    public sealed class Herd : BaseEntity
    {
        public int FarmId { get; private set; }

        public int HerdNumber { get; private set; }

        public HerdStatus Status { get; private set; }

        public string? Description { get; private set; }

        private Herd() { } // Required for EF Core

        private Herd(int farmId, int herdNumber, string? description)
        {
            if (farmId <= 0)
                throw new ArgumentException("FarmId must be valid.");

            if (herdNumber <= 0)
                throw new ArgumentException("Herd number must be greater than zero.");

            FarmId = farmId;
            HerdNumber = herdNumber;
            Description = description?.Trim();
            Status = HerdStatus.Active;
        }

        public static Herd Create(int farmId, int herdNumber, string? description)
        {
            return new Herd(farmId, herdNumber, description);
        }

        public void UpdateDescription(string? description)
        {
            Description = description?.Trim();
            SetUpdated();
        }

        public void Deactivate()
        {
            if (Status == HerdStatus.Inactive)
                throw new InvalidOperationException("Herd is already inactive.");

            Status = HerdStatus.Inactive;
            SetUpdated();
        }
    }
}
