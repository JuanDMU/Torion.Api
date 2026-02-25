using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Base;
using Torion.Domain.ValueObjects;

namespace Torion.Domain.Entities
{
    public sealed class VaccineAnimal : BaseEntity
    {
        public int AnimalId { get; private set; }

        public int VaccineId { get; private set; }

        public Dose Dose { get; private set; } = default!;

        public DateTime ApplicationDate { get; private set; }

        public string? Notes { get; private set; }

        private VaccineAnimal() { } // EF Core

        private VaccineAnimal(
            int animalId,
            int vaccineId,
            Dose dose,
            DateTime applicationDate,
            string? notes)
        {
            if (animalId <= 0)
                throw new ArgumentException("AnimalId must be valid.");

            if (vaccineId <= 0)
                throw new ArgumentException("VaccineId must be valid.");

            if (applicationDate.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Application date cannot be in the future.");

            AnimalId = animalId;
            VaccineId = vaccineId;
            Dose = dose;
            ApplicationDate = applicationDate.Date;
            Notes = notes?.Trim();
        }

        internal static VaccineAnimal Create(
            int animalId,
            int vaccineId,
            Dose dose,
            DateTime applicationDate,
            string? notes)
        {
            return new VaccineAnimal(animalId, vaccineId, dose, applicationDate, notes);
        }
    }
}
