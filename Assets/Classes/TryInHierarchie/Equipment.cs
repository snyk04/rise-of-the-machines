using UnityEngine;

namespace Classes.TryInHierarchie {
    public abstract class Equipment : IUpgradable {
        public string Name { get; }

        public int Level { get; private set; }
        
        protected Equipment(string name) {
            Name = name;
        }

        public virtual void Upgrade() {
            Level += 1;
            Debug.Log($"Upgrade {Name}");
        }
    }
}