using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Base;
using Torion.Domain.Enumerations;
using Torion.Domain.ValueObjects;

namespace Torion.Domain.Entities
{
    public sealed class Animal : BaseEntity
    {
        private readonly List<WeightRecord> _weightRecords = new();

        public int FarmId { get; private set; }

        public int HerdId { get; private set; }

        public AnimalCode AnimalCode { get; private set; } = default!;

        public Gender Gender { get; private set; }

        public Weight InitialWeight { get; private set; } = default!;

        public DateTime AdmissionDate { get; private set; }

        public AnimalStatus Status { get; private set; }

        public IReadOnlyCollection<WeightRecord> WeightRecords => _weightRecords.AsReadOnly();

        private Animal() { } // EF Core

        private Animal(
            int farmId,
            int herdId,
            AnimalCode animalCode,
            Gender gender,
            Weight initialWeight,
            DateTime admissionDate)
        {
            if (farmId <= 0)
                throw new ArgumentException("FarmId must be valid.");

            if (herdId <= 0)
                throw new ArgumentException("HerdId must be valid.");

            if (admissionDate.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Admission date cannot be in the future.");

            FarmId = farmId;
            HerdId = herdId;
            AnimalCode = animalCode;
            Gender = gender;
            InitialWeight = initialWeight;
            AdmissionDate = admissionDate.Date;
            Status = AnimalStatus.Alive;
        }

        public static Animal Create(
            int farmId,
            int herdId,
            AnimalCode animalCode,
            Gender gender,
            Weight initialWeight,
            DateTime admissionDate)
        {
            return new Animal(farmId, herdId, animalCode, gender, initialWeight, admissionDate);
        }

        public void AddWeightRecord(DateTime date, Weight weight, string? notes)
        {
            if (date.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Weight record date cannot be in the future.");

            if (_weightRecords.Any(w => w.Date == date.Date))
                throw new InvalidOperationException("A weight record already exists for this date.");

            var record = WeightRecord.Create(Id, date.Date, weight, notes);
            _weightRecords.Add(record);

            SetUpdated();
        }

        public void MarkAsSold()
        {
            if (Status == AnimalStatus.Sold)
                throw new InvalidOperationException("Animal is already sold.");

            Status = AnimalStatus.Sold;
            SetUpdated();
        }

        public void MarkAsDead()
        {
            if (Status == AnimalStatus.Dead)
                throw new InvalidOperationException("Animal is already dead.");

            Status = AnimalStatus.Dead;
            SetUpdated();
        }
    }
}
