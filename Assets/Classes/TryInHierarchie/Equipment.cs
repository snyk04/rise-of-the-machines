using UnityEngine;

namespace Classes.TryInHierarchie {
    public abstract class Equipment : IUpgradable {
        public string Name { get; }

        public int Level { get; private set; }

        private Health health;

        protected Equipment(string name, Health health) {
            Name = name;
            this.health = health;
        }

        public virtual void Upgrade() {
            Level += 1;
            Debug.Log($"Upgrade {Name}");
        }
    }
}