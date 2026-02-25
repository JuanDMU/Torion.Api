using System;
using System.Collections.Generic;
using System.Text;

namespace Torion.Domain.ValueObjects
{
    public sealed class AnimalCode : IEquatable<AnimalCode>
    {
        public string Value { get; }

        public AnimalCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("AnimalCode cannot be empty.");

            Value = value.Trim().ToUpperInvariant();
        }

        public override bool Equals(object? obj) =>
            obj is AnimalCode other && Equals(other);

        public bool Equals(AnimalCode? other) =>
            other is not null && Value == other.Value;

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() => Value;
    }
}
