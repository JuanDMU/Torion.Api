using System;
using System.Collections.Generic;
using System.Linq;
using Torion.Domain.Base;
using Torion.Domain.Enumerations;
using Torion.Domain.ValueObjects;

namespace Torion.Domain.Entities
{
    public sealed class Farm : BaseEntity
    {
        private readonly List<Herd> _herds = new();
        private readonly List<Animal> _animals = new();

        public int OwnerId { get; private set; }

        public FarmCode FarmCode { get; private set; } = default!;

        public string Name { get; private set; } = string.Empty;

        public string? Location { get; private set; }

        public IReadOnlyCollection<Herd> Herds => _herds.AsReadOnly();

        public IReadOnlyCollection<Animal> Animals => _animals.AsReadOnly();

        private Farm() { } // EF Core

        private Farm(int ownerId, FarmCode farmCode, string name, string? location)
        {
            if (ownerId <= 0)
                throw new ArgumentException("OwnerId must be valid.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Farm name is required.");

            OwnerId = ownerId;
            FarmCode = farmCode;
            Name = name.Trim();
            Location = location?.Trim();
        }

        public static Farm Create(int ownerId, FarmCode farmCode, string name, string? location)
        {
            return new Farm(ownerId, farmCode, name, location);
        }

        public Herd AddHerd(string name, string? description)
        {
            if (_herds.Any(h => h.Name.ToLower() == name.ToLower()))
                throw new InvalidOperationException("A herd with the same name already exists in this farm.");

            var herd = Herd.Create(Id, name, description);

            _herds.Add(herd);

            SetUpdated();

            return herd;
        }

        public Animal AddAnimal(
            int herdId,
            AnimalCode animalCode,
            Gender gender,
            Weight initialWeight,
            DateTime admissionDate)
        {
            if (!_herds.Any(h => h.Id == herdId))
                throw new InvalidOperationException("The specified herd does not belong to this farm.");

            if (_animals.Any(a => a.AnimalCode == animalCode))
                throw new InvalidOperationException("An animal with the same code already exists in this farm.");

            var animal = Animal.Create(
                Id,
                herdId,
                animalCode,
                gender,
                initialWeight,
                admissionDate);

            _animals.Add(animal);

            SetUpdated();

            return animal;
        }

        public void UpdateDetails(string name, string? location)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Farm name is required.");

            Name = name.Trim();
            Location = location?.Trim();

            SetUpdated();
        }
    }
}