using System;
using System.Collections.Generic;
using System.Text;

namespace Torion.Domain.ValueObjects
{
    public sealed class Money : IEquatable<Money>
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Money must be greater than zero.");

            Value = value;
        }

        public override bool Equals(object? obj) =>
            obj is Money other && Equals(other);

        public bool Equals(Money? other) =>
            other is not null && Value == other.Value;

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() => Value.ToString("F2");
    }
}
