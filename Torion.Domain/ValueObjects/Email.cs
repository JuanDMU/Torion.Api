using System;
using System.Collections.Generic;
using System.Text;

namespace Torion.Domain.ValueObjects
{
    public sealed class Email : IEquatable<Email>
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.");

            if (!value.Contains("@"))
                throw new ArgumentException("Invalid email format.");

            Value = value.Trim().ToLowerInvariant();
        }

        public override bool Equals(object? obj) =>
            obj is Email other && Equals(other);

        public bool Equals(Email? other) =>
            other is not null && Value == other.Value;

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() => Value;
    
    }
}
