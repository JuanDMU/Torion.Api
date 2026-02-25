using System;
using System.Collections.Generic;
using System.Text;

namespace Torion.Domain.ValueObjects
{
    public sealed class FarmCode : IEquatable<FarmCode>
    {
        public string Value { get; }

        public FarmCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("FarmCode cannot be empty.");

            Value = value.Trim().ToUpperInvariant();
        }

        public override bool Equals(object? obj) =>
            obj is FarmCode other && Equals(other);

        public bool Equals(FarmCode? other) =>
            other is not null && Value == other.Value;

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() => Value;
    }
}
