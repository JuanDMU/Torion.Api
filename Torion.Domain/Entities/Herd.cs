using System;
using System.Collections.Generic;
using System.Linq;
using Torion.Domain.Base;
using Torion.Domain.ValueObjects;
using Torion.Domain.Enumerations;

namespace Torion.Domain.Entities
{
    public sealed class Herd : BaseEntity
    {
        private readonly List<VaccineHerd> _vaccines = new();

        public int FarmId { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public HerdStatus Status { get; private set; }

        public IReadOnlyCollection<VaccineHerd> Vaccines => _vaccines.AsReadOnly();

        private Herd() { } // EF Core

        private Herd(int farmId, string name, string? description)
        {
            if (farmId <= 0)
                throw new ArgumentException("FarmId must be valid.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Herd name is required.");

            FarmId = farmId;
            Name = name.Trim();
            Description = description?.Trim();
            Status = HerdStatus.Active;
        }

        public static Herd Create(int farmId, string name, string? description)
        {
            return new Herd(farmId, name, description);
        }

        public void ApplyVaccine(int vaccineId, Dose dose, DateTime applicationDate, string? notes)
        {
            if (Status == HerdStatus.Inactive)
                throw new InvalidOperationException("Cannot apply vaccine to an inactive herd.");

            if (vaccineId <= 0)
                throw new ArgumentException("VaccineId must be valid.");

            if (applicationDate.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Application date cannot be in the future.");

            if (_vaccines.Any(v =>
                v.VaccineId == vaccineId &&
                v.ApplicationDate.Date == applicationDate.Date))
            {
                throw new InvalidOperationException(
                    "This vaccine has already been applied to this herd on the same date.");
            }

            var vaccine = VaccineHerd.Create(
                Id,
                vaccineId,
                dose,
                applicationDate.Date,
                notes);

            _vaccines.Add(vaccine);

            SetUpdated();
        }

        public void MarkAsInactive()
        {
            if (Status == HerdStatus.Inactive)
                throw new InvalidOperationException("Herd is already inactive.");

            Status = HerdStatus.Inactive;

            SetUpdated();
        }

        public void UpdateDetails(string name, string? description)
        {
            if (Status == HerdStatus.Inactive)
                throw new InvalidOperationException("Cannot modify an inactive herd.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Herd name is required.");

            Name = name.Trim();
            Description = description?.Trim();

            SetUpdated();
        }
    }
}