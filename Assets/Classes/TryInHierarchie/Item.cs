using UnityEngine;

namespace Classes.TryInHierarchie {
    public abstract class Item : ICanBePickedUp {

        public abstract bool TryPick();
    }
}