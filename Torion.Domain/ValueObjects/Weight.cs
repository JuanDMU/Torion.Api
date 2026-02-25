using System;
using System.Collections.Generic;
using System.Text;

namespace Torion.Domain.ValueObjects
{
    public sealed class Weight : IEquatable<Weight>
    {
        public decimal Value { get; }

        public Weight(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Weight must be greater than zero.");

            Value = value;
        }

        public override bool Equals(object? obj) =>
            obj is Weight other && Equals(other);

        public bool Equals(Weight? other) =>
            other is not null && Value == other.Value;

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() => $"{Value} kg";
    }
}
