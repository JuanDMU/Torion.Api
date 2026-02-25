using System;
using System.Collections.Generic;
using System.Text;
using Torion.Domain.Base;
using Torion.Domain.ValueObjects;
namespace Torion.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string FullName { get; private set; } = default!;

        public Email Email { get; private set; } = default!;

        public string PasswordHash { get; private set; } = default!;

        private User() { } // Required for EF Core

        private User(string fullName, Email email, string passwordHash)
        {
            SetFullName(fullName);
            Email = email;
            SetPasswordHash(passwordHash);
        }

        public static User Create(string fullName, Email email, string passwordHash)
        {
            return new User(fullName, email, passwordHash);
        }

        public void UpdateFullName(string fullName)
        {
            SetFullName(fullName);
            SetUpdated();
        }

        public void ChangePassword(string newPasswordHash)
        {
            SetPasswordHash(newPasswordHash);
            SetUpdated();
        }

        private void SetFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be empty.");

            if (fullName.Trim().Length < 3)
                throw new ArgumentException("Full name must be at least 3 characters.");

            FullName = fullName.Trim();
        }

        private void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash cannot be empty.");

            PasswordHash = passwordHash;
        }
    }
}
