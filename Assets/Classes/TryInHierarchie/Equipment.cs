using System.Collections.Generic;
using UnityEngine;
using Type = Classes.TryInHierarchie.Characteristic.Type;

namespace Classes.TryInHierarchie {
    public abstract class Equipment : IUpgradable, IHasCharacteristics, ICanBePickedUp {
        
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

        public bool TryPick() {
            return Player.Instance.Inventory.TryAdd(this);
        }
    }
}