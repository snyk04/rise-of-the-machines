using System.Collections.Generic;
using Project.Classes.Characteristics;
using Project.Classes.Slots;
using Project.Interfaces;
using UnityEngine;
using Type = Project.Classes.Characteristics.Characteristic.Type;

namespace Project.Classes.ItemsAndInventory.Equipment {
    public abstract class Equipment : Item, IUpgradable, IHasCharacteristics {
        public Dictionary<Type, Characteristic> Stats { get; private set; }

        public string Name { get; }

        public int Level { get; private set; }

        protected Equipment(string name, Dictionary<Type, Characteristic> stats) {
            Name = name;
            Stats = stats;
        }

        public abstract EquipmentSlot.Type RequiredSlot();

        public virtual void Upgrade() {
            Level += 1;
            Debug.Log($"Upgrade {Name}");
        }
    }
}