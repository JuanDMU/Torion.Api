using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Base;
using Torion.Domain.ValueObjects;

namespace Torion.Domain.Entities
{
    public sealed class VaccineHerd : BaseEntity
    {
        public int HerdId { get; private set; }

        public int VaccineId { get; private set; }

        public Dose Dose { get; private set; } = default!;

        public DateTime ApplicationDate { get; private set; }

        public string? Notes { get; private set; }

        private VaccineHerd() { } // EF Core

        private VaccineHerd(
            int herdId,
            int vaccineId,
            Dose dose,
            DateTime applicationDate,
            string? notes)
        {
            if (herdId <= 0)
                throw new ArgumentException("HerdId must be valid.");

            if (vaccineId <= 0)
                throw new ArgumentException("VaccineId must be valid.");

            if (applicationDate.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Application date cannot be in the future.");

            HerdId = herdId;
            VaccineId = vaccineId;
            Dose = dose;
            ApplicationDate = applicationDate.Date;
            Notes = notes?.Trim();
        }

        internal static VaccineHerd Create(
            int herdId,
            int vaccineId,
            Dose dose,
            DateTime applicationDate,
            string? notes)
        {
            return new VaccineHerd(herdId, vaccineId, dose, applicationDate, notes);
        }
    }
}
