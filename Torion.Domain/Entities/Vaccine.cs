using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Base;
using Torion.Domain.Enumerations;

namespace Torion.Domain.Entities
{
    public sealed class Vaccine : BaseEntity
    {
        public string Name { get; private set; } = default!;

        public VaccineType Type { get; private set; }

        public int WithdrawalDays { get; private set; }

        public string? Notes { get; private set; }

        private Vaccine() { } // EF Core

        private Vaccine(string name, VaccineType type, int withdrawalDays, string? notes)
        {
            SetName(name);

            if (withdrawalDays < 0)
                throw new ArgumentException("Withdrawal days cannot be negative.");

            Type = type;
            WithdrawalDays = withdrawalDays;
            Notes = notes?.Trim();
        }

        public static Vaccine Create(string name, VaccineType type, int withdrawalDays, string? notes)
        {
            return new Vaccine(name, type, withdrawalDays, notes);
        }

        public void UpdateDetails(string name, VaccineType type, int withdrawalDays, string? notes)
        {
            SetName(name);

            if (withdrawalDays < 0)
                throw new ArgumentException("Withdrawal days cannot be negative.");

            Type = type;
            WithdrawalDays = withdrawalDays;
            Notes = notes?.Trim();

            SetUpdated();
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Vaccine name cannot be empty.");

            if (name.Trim().Length < 3)
                throw new ArgumentException("Vaccine name must be at least 3 characters.");

            Name = name.Trim();
        }
    }
}
