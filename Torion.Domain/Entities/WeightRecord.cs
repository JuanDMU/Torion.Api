using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Base;
using Torion.Domain.ValueObjects;

namespace Torion.Domain.Entities
{
    public sealed class WeightRecord : BaseEntity
    {
        public int AnimalId { get; private set; }

        public DateTime Date { get; private set; }

        public Weight Weight { get; private set; } = default!;

        public string? Notes { get; private set; }

        private WeightRecord() { } // EF Core

        private WeightRecord(int animalId, DateTime date, Weight weight, string? notes)
        {
            if (animalId <= 0)
                throw new ArgumentException("AnimalId must be valid.");

            if (date.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Weight record date cannot be in the future.");

            AnimalId = animalId;
            Date = date.Date;
            Weight = weight;
            Notes = notes?.Trim();
        }

        internal static WeightRecord Create(int animalId, DateTime date, Weight weight, string? notes)
        {
            return new WeightRecord(animalId, date, weight, notes);
        }
    }
}
