using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Base;
using Torion.Domain.Enumerations;
using Torion.Domain.ValueObjects;

namespace Torion.Domain.Entities
{
    public sealed class Farm : BaseEntity
    {
        public int UserId { get; private set; }

        public FarmCode FarmCode { get; private set; } = default!;

        public string Name { get; private set; } = default!;

        public string? Location { get; private set; }

        public string? Description { get; private set; }

        public FarmStatus Status { get; private set; }

        private Farm() { } // Required for EF Core

        private Farm(int userId, FarmCode farmCode, string name, string? location, string? description)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId must be valid.");

            UserId = userId;
            FarmCode = farmCode;

            SetName(name);
            Location = location?.Trim(); // If location is provided, trim it
            Description = description?.Trim();

            Status = FarmStatus.Active;
        }

        public static Farm Create(int userId, FarmCode farmCode, string name, string? location, string? description)
        {
            return new Farm(userId, farmCode, name, location, description);
        }

        public void UpdateDetails(string name, string? location, string? description)
        {
            SetName(name);
            Location = location?.Trim();
            Description = description?.Trim();

            SetUpdated();
        }

        public void Deactivate()
        {
            if (Status == FarmStatus.Inactive)
                throw new InvalidOperationException("Farm is already inactive.");

            Status = FarmStatus.Inactive;
            SetUpdated();
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Farm name cannot be empty.");

            if (name.Trim().Length < 3)
                throw new ArgumentException("Farm name must be at least 3 characters.");

            Name = name.Trim();
        }
    }
}
