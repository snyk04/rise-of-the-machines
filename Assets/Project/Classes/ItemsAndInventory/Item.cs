using Project.Classes.Damagable;
using Project.Interfaces;

namespace Project.Classes.ItemsAndInventory {
    public abstract class Item : ICanBePickedUp {

        public virtual bool TryPick() {
            return Player.Instance.Inventory.TryAdd(this);
        }
    }
}