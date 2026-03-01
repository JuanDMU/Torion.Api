using System;
using Torion.Domain.Base;
using Torion.Domain.ValueObjects;

namespace Torion.Domain.Entities
{
    public sealed class Sale : BaseEntity
    {
        public int HerdId { get; private set; }

        public Money TotalPrice { get; private set; } = default!;

        public DateTime SaleDate { get; private set; }

        public string BuyerName { get; private set; } = string.Empty;

        public string? Notes { get; private set; }

        private Sale() { } // EF Core

        private Sale(
            int herdId,
            Money totalPrice,
            DateTime saleDate,
            string buyerName,
            string? notes)
        {
            if (herdId <= 0)
                throw new ArgumentException("HerdId must be valid.");

            if (saleDate.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Sale date cannot be in the future.");

            if (string.IsNullOrWhiteSpace(buyerName))
                throw new ArgumentException("Buyer name is required.");

            HerdId = herdId;
            TotalPrice = totalPrice;
            SaleDate = saleDate.Date;
            BuyerName = buyerName.Trim();
            Notes = notes?.Trim();
        }

        public static Sale Create(
            int herdId,
            Money totalPrice,
            DateTime saleDate,
            string buyerName,
            string? notes)
        {
            return new Sale(
                herdId,
                totalPrice,
                saleDate,
                buyerName,
                notes);
        }
    }
}