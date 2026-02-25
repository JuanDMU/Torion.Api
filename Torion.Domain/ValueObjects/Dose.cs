using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Enumerations;

namespace Torion.Domain.ValueObjects
{
    public sealed class Dose : IEquatable<Dose>
    {
        public decimal Amount { get; }
        public DoseUnit Unit { get; }

        public Dose(decimal amount, DoseUnit unit)
        {
            if (amount <= 0)
                throw new ArgumentException("Dose must be greater than zero.");

            Amount = amount;
            Unit = unit;
        }

        public override bool Equals(object? obj) =>
            obj is Dose other && Equals(other);

        public bool Equals(Dose? other) =>
            other is not null &&
            Amount == other.Amount &&
            Unit == other.Unit;

        public override int GetHashCode() =>
            HashCode.Combine(Amount, Unit);

        public override string ToString() => $"{Amount} {Unit}";
    }
}
