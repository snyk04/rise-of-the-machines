using System.Collections.Generic;

namespace Classes.TryInHierarchie {
    public class Inventory {
        private List<ICanBePickedUp> inventory = new List<ICanBePickedUp>();

        public bool TryAdd(ICanBePickedUp item) {
            if (false) { // todo some conditions
                return false;
            }
            inventory.Add(item);
            return true;
        }
        public List<ICanBePickedUp> GetContent() {
            return inventory;
            return new List<ICanBePickedUp>(inventory); // todo don't know is this ok
        }
    }
}