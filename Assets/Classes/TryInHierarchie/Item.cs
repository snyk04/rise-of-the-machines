using UnityEngine;

namespace Classes.TryInHierarchie {
    public abstract class Item : ICanBePickedUp {

        public virtual bool TryPick(Item item) {
            Debug.Log($"{item} was picked up");
            return true;
        }
    }
}