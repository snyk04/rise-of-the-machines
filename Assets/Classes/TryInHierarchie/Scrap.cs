using UnityEngine;

namespace Classes.TryInHierarchie {
    public class Scrap : Item {
        public override bool TryPick() {
            Debug.Log($"{this} was picked up");
            return Player.Instance.Inventory.TryAdd(this);
        }
    }
}