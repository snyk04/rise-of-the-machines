using Project.Classes.Damagable;
using UnityEngine;

namespace Project.Classes.CanBePickedUp.Items {
    public class Scrap : Item {
        public override bool TryPick() {
            Debug.Log($"{this} was picked up");
            return Player.Instance.Inventory.TryAdd(this);
        }
    }
}